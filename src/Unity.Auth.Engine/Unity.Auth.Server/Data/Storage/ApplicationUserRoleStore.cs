// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-08-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 11-22-2017
// ***********************************************************************
// <copyright file="ApplicationUserRoleStore.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Data.Storage
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Unity.Auth.Server.Data.Models;

#pragma warning disable CS1584 // XML comment has syntactically incorrect cref attribute
    /// <summary>
    /// Class ApplicationUserRoleStore.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.RoleStore{Models.ApplicationUserRole}" />
    public class ApplicationUserRoleStore : RoleStore<ApplicationUserRole>
#pragma warning restore CS1584 // XML comment has syntactically incorrect cref attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserRoleStore"/> class.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        public ApplicationUserRoleStore(UnityAuthDbContext ctx)
            : base(ctx)
        {
        }
    }
}
