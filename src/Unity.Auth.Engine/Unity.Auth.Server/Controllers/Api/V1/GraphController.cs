// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : haris.md
// Created          : 12-11-2017
//
// Last Modified By : haris.md
// Last Modified On : 12-11-2017
// ***********************************************************************
// <copyright file="GraphController.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Controllers.Api
{
    using System.Linq;
    using IdentityServer4.AccessTokenValidation;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Unity.Auth.Server.Data.Models;
    using Unity.Auth.Services;

    /// <summary>
    /// Class GraphController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/v1.0/Graph")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public class GraphController : Controller
    {
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// The email sender
        /// </summary>
        private readonly IEmailSender emailSender;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphController" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="emailSender">The email sender.</param>
        /// <param name="logger">The logger.</param>
        public GraphController(
          UserManager<ApplicationUser> userManager,
          IEmailSender emailSender,
          ILogger<GraphController> logger)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.logger = logger;
        }

#if DEBUG
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>User Claims [DEBUG]</returns>
        public IActionResult Get()
        {
            var claims = this.User.Claims.Select(c => new { c.Type, c.Value });
            return new JsonResult(claims);
        }
#endif
    }
}