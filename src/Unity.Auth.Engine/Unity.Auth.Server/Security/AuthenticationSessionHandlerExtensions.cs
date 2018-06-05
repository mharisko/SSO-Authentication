// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-20-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 12-12-2017
// ***********************************************************************
// <copyright file="AuthenticationSessionHandlerExtensions.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Security
{
    using Microsoft.AspNetCore.Builder;

    /// <summary>
    /// Class AuthenticationSessionHandlerExtensions.
    /// </summary>
    public static class AuthenticationSessionHandlerExtensions
    {
        /// <summary>
        /// Uses the authentication session handler.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>IApplicationBuilder.</returns>
        public static IApplicationBuilder UseAuthenticationSessionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationSessionHandler>();
        }
    }
}
