// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-20-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 12-12-2017
// ***********************************************************************
// <copyright file="TokenCleanup.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Unity.Auth.Server.Data.Storage;
    using Unity.Auth.Server.Options;

    /// <summary>
    /// Class TokenCleanup.
    /// </summary>
    internal class TokenCleanup
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<TokenCleanup> logger;

        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// The options
        /// </summary>
        private readonly OperationalServiceOptions options;

        /// <summary>
        /// The source
        /// </summary>
        private CancellationTokenSource source;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenCleanup" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="options">The options.</param>
        /// <exception cref="ArgumentNullException">options
        /// or
        /// logger
        /// or
        /// serviceProvider</exception>
        /// <exception cref="ArgumentException">Token cleanup interval must be at least 1 second
        /// or
        /// Token cleanup batch size interval must be at least 1</exception>
        public TokenCleanup(IServiceProvider serviceProvider, ILogger<TokenCleanup> logger, OperationalServiceOptions options)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            if (this.options.TokenCleanupInterval < 1)
            {
                throw new ArgumentException("Token cleanup interval must be at least 1 second");
            }

            if (this.options.TokenCleanupBatchSize < 1)
            {
                throw new ArgumentException("Token cleanup batch size interval must be at least 1");
            }

            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <summary>
        /// Gets the cleanup interval.
        /// </summary>
        /// <value>The cleanup interval.</value>
        public TimeSpan CleanupInterval => TimeSpan.FromSeconds(this.options.TokenCleanupInterval);

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            Start(CancellationToken.None);
        }

        /// <summary>
        /// Starts the specified cancellation token.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="InvalidOperationException">Already started. Call Stop first.</exception>
        public void Start(CancellationToken cancellationToken)
        {
            if (this.source != null)
            {
                throw new InvalidOperationException("Already started. Call Stop first.");
            }

            this.logger.LogDebug("Starting token cleanup");

            this.source = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            Task.Factory.StartNew(() => StartInternal(this.source.Token));
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        /// <exception cref="InvalidOperationException">Not started. Call Start first.</exception>
        public void Stop()
        {
            if (this.source == null)
            {
                throw new InvalidOperationException("Not started. Call Start first.");
            }

            this.logger.LogDebug("Stopping token cleanup");

            this.source.Cancel();
            this.source = null;
        }

        /// <summary>
        /// Clears the tokens.
        /// </summary>
        public void ClearTokens()
        {
            try
            {
                this.logger.LogTrace("Querying for tokens to clear");

                var found = int.MaxValue;

                using (var serviceScope = this.serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetService<UnityAuthDbContext>())
                    {
                        while (found >= this.options.TokenCleanupBatchSize)
                        {
                            var expired = context.UserSessions
                                .Where(x => x.Expiration < DateTime.UtcNow)
                                .OrderBy(x => x.Id)
                                .Take(this.options.TokenCleanupBatchSize)
                                .ToArray();

                            found = expired.Length;
                            this.logger.LogInformation("Clearing {tokenCount} tokens", found);

                            if (found > 0)
                            {
                                context.UserSessions.RemoveRange(expired);
                                try
                                {
                                    context.SaveChanges();
                                }
                                catch (DbUpdateConcurrencyException ex)
                                {
                                    // we get this if/when someone else already deleted the records
                                    // we want to essentially ignore this, and keep working
                                    this.logger.LogDebug("Concurrency exception clearing tokens: {exception}", ex.Message);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Exception clearing tokens: {exception}", ex.Message);
            }
        }

        /// <summary>
        /// Starts the internal.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        private async Task StartInternal(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    this.logger.LogDebug("CancellationRequested. Exiting.");
                    break;
                }

                try
                {
                    await Task.Delay(this.CleanupInterval, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    this.logger.LogDebug("TaskCanceledException. Exiting.");
                    break;
                }
                catch (Exception ex)
                {
                    this.logger.LogError("Task.Delay exception: {0}. Exiting.", ex.Message);
                    break;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    this.logger.LogDebug("CancellationRequested. Exiting.");
                    break;
                }

                ClearTokens();
            }
        }
    }
}
