// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 01-16-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 01-16-2017
// ***********************************************************************
// <copyright file="AuthorizeEngineAdminAttribute.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Filters
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Unity.Auth.Server.Configuration.Constants;

    /// <summary>
    /// Authorize Engine as administrator.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    internal class AuthorizeEngineAdminAttribute : Attribute, IAuthorizationFilter, IAsyncAuthorizationFilter
    {
        /// <inheritdoc/>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user != null && user.Identity.IsAuthenticated)
            {
                if (!user.IsInRole(EngineRoles.EngineAdminRole))
                {
                    context.Result = new RedirectToActionResult("Forbidden", "Home", new { url = context.HttpContext.Request.Path.Value });
                }
            }
        }

        /// <inheritdoc/>
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            this.OnAuthorization(context);
            return Task.CompletedTask;
        }
    }
}