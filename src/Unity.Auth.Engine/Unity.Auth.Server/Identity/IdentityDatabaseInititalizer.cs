// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-08-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 12-12-2017
// ***********************************************************************
// <copyright file="IdentityDatabaseInititalizer.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Identity
{
    using System.Linq;
    using System.Threading.Tasks;
    using IdentityServer4.Test;
    using Unity.Auth.Server.AppStartup;
    using Unity.Auth.Server.Data.Models;

    /// <summary>
    /// Class IdentityDatabaseInititalizer.
    /// </summary>
    /// <seealso cref="Unity.Auth.Server.Identity.IIdentityDatabaseInitializer" />
    public class IdentityDatabaseInititalizer : IIdentityDatabaseInitializer
    {
        /// <summary>
        /// The role manager
        /// </summary>
        private readonly ApplicationUserRoleManager roleManager;
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly ApplicationUserManager userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityDatabaseInititalizer"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        public IdentityDatabaseInititalizer(ApplicationUserManager userManager, ApplicationUserRoleManager roleManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; } = 1;

        /// <summary>
        /// seed as an asynchronous operation.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task SeedAsync()
        {
            if (!await this.roleManager.RoleExistsAsync("Admin"))
            {
                await this.roleManager.CreateAsync(new ApplicationUserRole { Id = "adm", Name = "Admin" });
            }

            if (!await this.roleManager.RoleExistsAsync("Employee"))
            {
                await this.roleManager.CreateAsync(new ApplicationUserRole { Id = "Employee", Name = "Employee" });
            }

            if (!await this.roleManager.RoleExistsAsync("Manager"))
            {
                await this.roleManager.CreateAsync(new ApplicationUserRole { Id = "Manager", Name = "Manager" });
            }


            foreach (TestUser user in Defaults.GetUsers())
            {
                if (await this.userManager.FindByNameAsync(user.Username) == null)
                {
                    ApplicationUser appUser = new ApplicationUser
                    {
                        UserName = user.Username,
                        Email = user.Claims.First(s => string.Compare(s.Type, System.Security.Claims.ClaimTypes.Email, true) == 0).Value,
                    };

                    var result = await this.userManager.CreateAsync(appUser, user.Password);
                    if (result.Succeeded)
                    {
                        var result1 = await this.userManager.AddToRoleAsync(appUser, "Admin");
                        foreach (System.Security.Claims.Claim claim in user.Claims)
                        {
                            await this.userManager.AddClaimAsync(appUser, claim);
                        }
                    }
                }
            }
        }
    }
}
