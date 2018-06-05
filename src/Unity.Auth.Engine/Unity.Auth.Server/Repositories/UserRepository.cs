// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-20-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 11-22-2017
// ***********************************************************************
// <copyright file="UserRepository.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Unity.Auth.Server.Data.Models;
    using Unity.Auth.Server.Data.Storage;
    using Unity.Auth.Server.Identity.Models;
    using Unity.Auth.Server.Repositories.Base;

    /// <summary>
    /// Class UserRepository.
    /// </summary>
    /// <seealso cref="Repository" />
    /// <seealso cref="Unity.Auth.Server.Repositories.IUserRepository" />
    public class UserRepository : Repository, IUserRepository
    {
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly ApplicationUserManager userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="unityAuthDbContext">The unity authentication database context.</param>
        /// <param name="userManager">The user manager.</param>
        public UserRepository(UnityAuthDbContext unityAuthDbContext, ApplicationUserManager userManager)
            : base(unityAuthDbContext)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>Task&lt;ApplicationUser&gt;.</returns>
        public Task<ApplicationUser> FindAsync(string userName)
        {
            return this.userManager.FindByNameAsync(userName);
        }

        /// <summary>
        /// Gets the active sessions.
        /// </summary>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        public Task<int> GetActiveSessions()
        {
            return this.DbContext.UserSessions.CountAsync();
        }

        /// <summary>
        /// Gets the find registered users.
        /// </summary>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        public Task<int> GetFindRegisteredUsers()
        {
            return this.DbContext.Users.CountAsync();
        }

        /// <summary>
        /// Saves the session.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="sessionExpiryTime">The session expiry time.</param>
        /// <returns>Task.</returns>
        public async Task SaveSession(string userId, DateTime sessionExpiryTime)
        {
            await this.DbContext.UserSessions.AddAsync(new UserSession
            {
                UserId = userId,
                Expiration = sessionExpiryTime
            });

            await this.DbContext.SaveChangesAsync();
        }
    }
}
