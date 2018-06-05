// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-20-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 11-22-2017
// ***********************************************************************
// <copyright file="SessionManagerAttribute.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Filters
{
    using System;
    using System.Threading.Tasks;
    using IdentityServer4.Extensions;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Unity.Auth.Server.Configuration.Constants;
    using Unity.Auth.Server.Repositories;

    /// <summary>
    /// Class SessionManagerAttribute.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
    public class SessionManagerAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// The client repository
        /// </summary>
        private readonly IClientRepository clientRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionManagerAttribute" /> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="clientRepository">The client repository.</param>
        public SessionManagerAttribute(IUserRepository userRepository, IClientRepository clientRepository)
        {
            this.userRepository = userRepository;
            this.clientRepository = clientRepository;
        }

        /// <summary>
        /// on action execution as an asynchronous operation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="next">The next.</param>
        /// <returns>Task.</returns>
        /// <inheritdoc />
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await this.clientRepository.SaveServerUsage(new Identity.Models.ServerUsage { ConnectedTime = DateTime.UtcNow }).ConfigureAwait(false);
            if ((context.HttpContext.User?.Identity?.IsAuthenticated).GetValueOrDefault() == true)
            {
                var userId = context.HttpContext.User.GetSubjectId();
                await this.userRepository.SaveSession(userId, DateTime.UtcNow.AddMinutes(SessionSecurity.SessionExpiryTime)).ConfigureAwait(false);
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
