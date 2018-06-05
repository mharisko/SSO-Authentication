// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-20-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 11-22-2017
// ***********************************************************************
// <copyright file="IUserRepository.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Unity.Auth.Server.Data.Models;

    /// <summary>
    /// Interface IUserRepository
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>Task&lt;ApplicationUser&gt;.</returns>
        Task<ApplicationUser> FindAsync(string userName);

        /// <summary>
        /// Saves the session.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="sessionExpiryTime">The session expiry time.</param>
        /// <returns>Task.</returns>
        Task SaveSession(string userId, DateTime sessionExpiryTime);

        /// <summary>
        /// Gets the active sessions.
        /// </summary>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        Task<int> GetActiveSessions();

        /// <summary>
        /// Gets the find registered users.
        /// </summary>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        Task<int> GetFindRegisteredUsers();
    }
}