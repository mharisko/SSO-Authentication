// <copyright file="HomeController.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>

namespace Unity.Auth.Server.Controllers
{
    using System.Threading.Tasks;
    using IdentityServer4.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Unity.Auth.Server.Controllers.Home;
    using Unity.Auth.Server.Filters;
    using Unity.Auth.Server.Models;
    using Unity.Auth.Server.Repositories;

    [SecurityHeaders]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService interaction;
        private readonly IUserRepository userRepository;
        private readonly IClientRepository clientRepository;
        private readonly DashboardService dashboardService;

        public HomeController(IIdentityServerInteractionService interaction, IUserRepository userRepository, IClientRepository clientRepository)
        {
            this.interaction = interaction;
            this.userRepository = userRepository;
            this.clientRepository = clientRepository;
            this.dashboardService = new DashboardService(this.userRepository, this.clientRepository);
        }

        public async Task<IActionResult> Index()
        {
            var view = await this.dashboardService.PrepareDashboardView();
            return View(view);
        }

        [AllowAnonymous]
        public IActionResult NotFound(string url)
        {
            this.ViewBag.RequestedUrl = url;
            return View("NotFound");
        }

        public IActionResult Forbidden(string url)
        {
            this.ViewBag.RequestedUrl = url;
            return View("Forbidden");
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await this.interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
            }

            return View("Error", vm);
        }
    }
}