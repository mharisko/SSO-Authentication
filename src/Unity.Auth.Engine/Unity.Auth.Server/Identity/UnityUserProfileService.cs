// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-20-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 12-12-2017
// ***********************************************************************
// <copyright file="UnityUserProfileService.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Identity
{
    using System.Linq;
    using System.Threading.Tasks;
    using IdentityServer4.Extensions;
    using IdentityServer4.Models;
    using IdentityServer4.Services;
    using Microsoft.Extensions.Logging;
    using Unity.Auth.Server.Data.Models;

    /// <summary>
    /// Class UnityUserProfileService.
    /// </summary>
    /// <seealso cref="IdentityServer4.Services.IProfileService" />
    public class UnityUserProfileService : IProfileService
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The user manager
        /// </summary>
        private readonly ApplicationUserManager userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityUserProfileService" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="userManager">The user manager.</param>
        public UnityUserProfileService(ILogger<UnityUserProfileService> logger, ApplicationUserManager userManager)
        {
            this.logger = logger;
            this.userManager = userManager;
        }

        /// <summary>
        /// This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Task.</returns>
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.LogProfileRequest(this.logger);
            context.AddRequestedClaims(context.Subject.Claims);
            context.LogIssuedClaims(this.logger);

            if (context.RequestedClaimTypes.Any())
            {
                var user = await this.userManager.FindByIdAsync(context.Subject.GetSubjectId());
                if (user != null)
                {
                    var claims = await this.userManager.GetClaimsAsync(user);
                    context.AddRequestedClaims(claims);
                }
            }

            context.LogIssuedClaims(this.logger);
        }

        /// <summary>
        /// This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in).
        /// (e.g. during token issuance or validation).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Task.</returns>
        public async Task IsActiveAsync(IsActiveContext context)
        {
            this.logger.LogDebug("IsActive called from: {caller}", context.Caller);

            var user = await this.userManager.FindByIdAsync(context.Subject.GetSubjectId());
            context.IsActive = user?.IsActive == true;
        }
    }
}
