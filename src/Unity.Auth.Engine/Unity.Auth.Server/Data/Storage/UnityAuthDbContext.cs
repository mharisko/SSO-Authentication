// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-08-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 11-21-2017
// ***********************************************************************
// <copyright file="UnityAuthDbContext.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Data.Storage
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Unity.Auth.Server.Data.Models;
    using Unity.Auth.Server.Identity.Models;

#pragma warning disable CS1584 // XML comment has syntactically incorrect cref attribute
    /// <summary>
    /// Class UnityAuthDbContext.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext{Models.ApplicationUser, Models.ApplicationUserRole, string}" />
    /// <seealso cref="IdentityDbContext" />
    public class UnityAuthDbContext : IdentityDbContext<ApplicationUser, ApplicationUserRole, string>
#pragma warning restore CS1584 // XML comment has syntactically incorrect cref attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnityAuthDbContext" /> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public UnityAuthDbContext(DbContextOptions<UnityAuthDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the user sessions.
        /// </summary>
        /// <value>The user sessions.</value>
        public DbSet<UserSession> UserSessions { get; set; }

        /// <summary>
        /// Gets or sets the server usages.
        /// </summary>
        /// <value>The server usages.</value>
        public DbSet<ServerUsage> ServerUsages { get; set; }
    }
}
