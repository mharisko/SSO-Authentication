// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : haris.md
// Created          : 12-11-2017
//
// Last Modified By : haris.md
// Last Modified On : 12-11-2017
// ***********************************************************************
// <copyright file="TokenCleanupHost.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.AppStartup
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Unity.Auth.Server.Options;

    /// <summary>
    /// Class TokenCleanupHost.
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Hosting.IHostedService" />
    internal class TokenCleanupHost : IHostedService
    {
        /// <summary>
        /// The token cleanup
        /// </summary>
        private readonly TokenCleanup tokenCleanup;

        /// <summary>
        /// The options
        /// </summary>
        private readonly OperationalServiceOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenCleanupHost" /> class.
        /// </summary>
        /// <param name="tokenCleanup">The token cleanup.</param>
        /// <param name="options">The options.</param>
        public TokenCleanupHost(TokenCleanup tokenCleanup, OperationalServiceOptions options)
        {
            this.tokenCleanup = tokenCleanup;
            this.options = options;
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        /// <inheritdoc />
        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (this.options.EnableTokenCleanup)
            {
                this.tokenCleanup.Start(cancellationToken);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        /// <inheritdoc />
        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (this.options.EnableTokenCleanup)
            {
                this.tokenCleanup.Stop();
            }

            return Task.CompletedTask;
        }
    }
}
