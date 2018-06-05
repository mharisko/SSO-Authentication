// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-08-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 11-21-2017
// ***********************************************************************
// <copyright file="ApplicationUserRoleManager.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Data.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;

#pragma warning disable CS1584 // XML comment has syntactically incorrect cref attribute
    /// <summary>
    /// Class ApplicationUserRoleManager.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.RoleManager{Models.ApplicationUserRole}" />
    public class ApplicationUserRoleManager : RoleManager<ApplicationUserRole>
#pragma warning restore CS1584 // XML comment has syntactically incorrect cref attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserRoleManager"/> class.
        /// </summary>
        /// <param name="store">The persistence store the manager will operate over.</param>
        /// <param name="roleValidators">A collection of validators for roles.</param>
        /// <param name="keyNormalizer">The normalizer to use when normalizing role names to keys.</param>
        /// <param name="errors">The <see cref="T:Microsoft.AspNetCore.Identity.IdentityErrorDescriber" /> used to provider error messages.</param>
        /// <param name="logger">The logger used to log messages, warnings and errors.</param>
        public ApplicationUserRoleManager(
            IRoleStore<ApplicationUserRole> store,
            IEnumerable<IRoleValidator<ApplicationUserRole>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManager<ApplicationUserRole>> logger)
            : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }

        /// <summary>
        /// Finds the role associated with the specified <paramref name="roleId" /> if any.
        /// </summary>
        /// <param name="roleId">The role ID whose role should be returned.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the role
        /// associated with the specified <paramref name="roleId" /></returns>
        public override Task<ApplicationUserRole> FindByIdAsync(string roleId)
        {
            return base.FindByIdAsync(roleId);
        }
    }
}
