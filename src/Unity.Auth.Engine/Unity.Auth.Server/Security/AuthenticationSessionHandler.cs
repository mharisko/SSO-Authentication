// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-20-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 12-12-2017
// ***********************************************************************
// <copyright file="AuthenticationSessionHandler.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Security
{
    using System;
    using System.Threading.Tasks;
    using IdentityServer4.Extensions;
    using Microsoft.AspNetCore.Http;
    using Unity.Auth.Server.Configuration.Constants;
    using Unity.Auth.Server.Repositories;

    /// <summary>
    /// Class AuthenticationSessionHandler.
    /// </summary>
    public class AuthenticationSessionHandler
    {
        /// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationSessionHandler" /> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public AuthenticationSessionHandler(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Invokes the specified HTTP context.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="clientRepository">The client repository.</param>
        /// <returns>Task.</returns>
        public async Task Invoke(HttpContext httpContext, IUserRepository userRepository, IClientRepository clientRepository)
        {
            await clientRepository.SaveServerUsage(new Identity.Models.ServerUsage { ConnectedTime = DateTime.UtcNow }).ConfigureAwait(false);
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var userId = httpContext.User.GetSubjectId();
                await userRepository.SaveSession(userId, DateTime.UtcNow.AddMinutes(SessionSecurity.SessionExpiryTime)).ConfigureAwait(false);
            }

            await this.next(httpContext).ConfigureAwait(false);
        }
    }
}
