// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-08-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 11-22-2017
// ***********************************************************************
// <copyright file="AuthJwtBearerEvents.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Identity
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication.JwtBearer;

    /// <summary>
    /// Class AuthJwtBearerEvents.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents" />
    public class AuthJwtBearerEvents : JwtBearerEvents
    {
        /// <summary>
        /// Authentications the failed.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Task.</returns>
        public override async Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            await base.AuthenticationFailed(context);
        }

        /// <summary>
        /// Tokens the validated.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Task.</returns>
        public override async Task TokenValidated(TokenValidatedContext context)
        {
            await base.TokenValidated(context);
        }

        /// <summary>
        /// Messages the received.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Task.</returns>
        public override async Task MessageReceived(MessageReceivedContext context)
        {
            await base.MessageReceived(context);
        }
    }
}
