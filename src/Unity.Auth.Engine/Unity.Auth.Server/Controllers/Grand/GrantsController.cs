// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : haris.md
// Created          : 11-20-2017
//
// Last Modified By : haris.md
// Last Modified On : 12-12-2017
// ***********************************************************************
// <copyright file="GrantsController.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using IdentityServer4;
    using IdentityServer4.Services;
    using IdentityServer4.Stores;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Unity.Auth.Server.Filters;
    using Unity.Auth.Server.Models;

    /// <summary>
    /// This sample controller allows a user to revoke grants given to clients
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [SecurityHeaders]
    [Authorize(AuthenticationSchemes = IdentityServerConstants.DefaultCookieAuthenticationScheme)]
    public class GrantsController : Controller
    {
        /// <summary>
        /// The interaction
        /// </summary>
        private readonly IIdentityServerInteractionService interaction;

        /// <summary>
        /// The clients
        /// </summary>
        private readonly IClientStore clients;

        /// <summary>
        /// The resources
        /// </summary>
        private readonly IResourceStore resources;

        /// <summary>
        /// Initializes a new instance of the <see cref="GrantsController"/> class.
        /// </summary>
        /// <param name="interaction">The interaction.</param>
        /// <param name="clients">The clients.</param>
        /// <param name="resources">The resources.</param>
        public GrantsController(
            IIdentityServerInteractionService interaction,
            IClientStore clients,
            IResourceStore resources)
        {
            this.interaction = interaction;
            this.clients = clients;
            this.resources = resources;
        }

        /// <summary>
        /// Show list of grants
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View("Index", await BuildViewModelAsync());
        }

        /// <summary>
        /// Handle postback to revoke a client
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Revoke(string clientId)
        {
            await this.interaction.RevokeUserConsentAsync(clientId);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// build view model as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;GrantsViewModel&gt;.</returns>
        private async Task<GrantsViewModel> BuildViewModelAsync()
        {
            var grants = await this.interaction.GetAllUserConsentsAsync();

            var list = new List<GrantViewModel>();
            foreach (var grant in grants)
            {
                var client = await this.clients.FindClientByIdAsync(grant.ClientId);
                if (client != null)
                {
                    var resources = await this.resources.FindResourcesByScopeAsync(grant.Scopes);

                    var item = new GrantViewModel()
                    {
                        ClientId = client.ClientId,
                        ClientName = client.ClientName ?? client.ClientId,
                        ClientLogoUrl = client.LogoUri,
                        ClientUrl = client.ClientUri,
                        Created = grant.CreationTime,
                        Expires = grant.Expiration,
                        IdentityGrantNames = resources.IdentityResources.Select(x => x.DisplayName ?? x.Name).ToArray(),
                        ApiGrantNames = resources.ApiResources.Select(x => x.DisplayName ?? x.Name).ToArray()
                    };

                    list.Add(item);
                }
            }

            return new GrantsViewModel
            {
                Grants = list
            };
        }
    }
}