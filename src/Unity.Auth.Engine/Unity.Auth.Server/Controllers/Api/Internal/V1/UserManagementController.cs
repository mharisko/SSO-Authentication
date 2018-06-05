// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : haris.md
// Created          : 12-11-2017
//
// Last Modified By : haris.md
// Last Modified On : 12-11-2017
// ***********************************************************************
// <copyright file="UserManagementController.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Controllers.Api.Internal.V1
{
    using System;
    using System.Threading.Tasks;
    using IdentityServer4.AccessTokenValidation;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Unity.Auth.Server.Controllers.Api.Models;
    using Unity.Auth.Server.Data.Models;
    using Unity.Auth.Services;

    /// <summary>
    /// Class UserManagementController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/v1.0/user")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public class UserManagementController : Controller
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
        /// Initializes a new instance of the <see cref="UserManagementController" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="emailSender">The email sender.</param>
        /// <param name="logger">The logger.</param>
        public UserManagementController(
          UserManager<ApplicationUser> userManager,
          IEmailSender emailSender,
          ILogger<GraphController> logger)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the user by user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="ApplicationException">Could not found a use with given user name.</exception>
        public async Task<IActionResult> GetUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return this.BadRequest();
            }

            var applicationUser = await this.userManager.FindByNameAsync(userName);
            if (applicationUser == null)
            {
                throw new ApplicationException($"Could not found a use with named {userName}.");
            }

            UserEditModel model = new UserEditModel
            {
                Email = applicationUser.Email,
                UserName = applicationUser.UserName,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                PhoneNumber = applicationUser.PhoneNumber,
            };

            return this.Ok(model);
        }

        /// <summary>
        /// Adds the user to the authentication server.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="ApplicationException">The given details already exists with other user.</exception>
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserModel userModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var user = await this.userManager.FindByNameAsync(userModel.UserName);
            if (user != null)
            {
                throw new ApplicationException($"The user with user name '{userModel.UserName}' already exits.");
            }

            user = await this.userManager.FindByEmailAsync(userModel.Email);
            if (user != null)
            {
                throw new ApplicationException($"The user with email id'{userModel.UserName}' already exits.");
            }

            // user = await this.userManager.FindByPhoneNumberAsync(userModel.PhoneNumber);
            // if (user != null)
            // {
            //    throw new ApplicationException($"The user with email id'{userModel.UserName}' already exits.");
            // }
            ApplicationUser applicationUser = new ApplicationUser
            {
                Email = userModel.Email,
                UserName = userModel.UserName,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                PhoneNumber = userModel.PhoneNumber
            };

            var creationList = await this.userManager.CreateAsync(applicationUser, userModel.ConfirmPassword);
            if (creationList.Succeeded)
            {
                return Ok(new { Success = true });
            }
            else
            {
                foreach (IdentityError error in creationList.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, $"{error.Description}:{error.Code}");
                }

                return this.BadRequest();
            }
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="ApplicationException">The given user details could not found.</exception>
        [HttpPut]
        public async Task<IActionResult> UpdateUser(AddUserModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var user = await this.userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                throw new ApplicationException($"Could not found a use with named {model.UserName}.");
            }

            if (model.Email != user.Email)
            {
                var setEmailResult = await this.userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            if (model.PhoneNumber != user.PhoneNumber)
            {
                var setPhoneNumberResult = await this.userManager.SetEmailAsync(user, model.Email);
                if (!setPhoneNumberResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }

            bool changed = false;
            if (model.FirstName != user.FirstName)
            {
                user.FirstName = model.FirstName;
                changed = true;
            }

            if (model.LastName != user.LastName)
            {
                user.LastName = model.LastName;
                changed = true;
            }

            if (changed)
            {
                var updateUserResult = await this.userManager.UpdateAsync(user);
                if (!updateUserResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred while updating user with ID '{user.Id}'.");
                }
            }

            return this.Ok();
        }

        /// <summary>
        /// Removes the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        /// <exception cref="ApplicationException">Could not found a use with given user name.</exception>
        [HttpDelete]
        public async Task<IActionResult> RemoveUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return this.BadRequest();
            }

            var user = await this.userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new ApplicationException($"Could not found a user with named {userName}.");
            }

            var deleteUserResult = await this.userManager.DeleteAsync(user);
            if (deleteUserResult.Succeeded)
            {
                return Ok(new { Success = true });
            }
            else
            {
                foreach (IdentityError error in deleteUserResult.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, $"{error.Description}:{error.Code}");
                }

                return this.BadRequest();
            }
        }
    }
}