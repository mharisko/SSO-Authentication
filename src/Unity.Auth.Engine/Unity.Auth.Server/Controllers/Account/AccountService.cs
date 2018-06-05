// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : haris.md
// Created          : 12-11-2017
//
// Last Modified By : haris.md
// Last Modified On : 12-12-2017
// ***********************************************************************
// <copyright file="AccountService.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Controllers
{
    using System;
    using System.Threading.Tasks;
    using IdentityServer4.Services;
    using IdentityServer4.Stores;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Unity.Auth.Server.Controllers.Models;
    using Unity.Auth.Server.Helpers;

    /// <summary>
    /// Class AccountService.
    /// </summary>
    public class AccountService
    {
        /// <summary>
        /// The client store
        /// </summary>
        private readonly IClientStore clientStore;

        /// <summary>
        /// The interaction
        /// </summary>
        private readonly IIdentityServerInteractionService interaction;

        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// The scheme provider
        /// </summary>
        private readonly IAuthenticationSchemeProvider schemeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService" /> class.
        /// </summary>
        /// <param name="interaction">The interaction.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="schemeProvider">The scheme provider.</param>
        /// <param name="clientStore">The client store.</param>
        public AccountService(
            IIdentityServerInteractionService interaction,
            IHttpContextAccessor httpContextAccessor,
            IAuthenticationSchemeProvider schemeProvider,
            IClientStore clientStore)
        {
            this.interaction = interaction;
            this.httpContextAccessor = httpContextAccessor;
            this.schemeProvider = schemeProvider;
            this.clientStore = clientStore;
        }

        /// <summary>
        /// Builds the logout view model asynchronous.
        /// </summary>
        /// <param name="logoutId">The logout identifier.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            var user = this.httpContextAccessor.HttpContext.User;
            if (user?.Identity.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await this.interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return vm;
        }

        /// <summary>
        /// Builds the logged out view model asynchronous.
        /// </summary>
        /// <param name="logoutId">The logout identifier.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await this.interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = logout?.ClientId,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            return vm;
        }
    }
}