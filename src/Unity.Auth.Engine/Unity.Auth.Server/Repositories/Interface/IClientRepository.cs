// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-20-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 12-12-2017
// ***********************************************************************
// <copyright file="IClientRepository.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Unity.Auth.Server.Identity.Models;

    /// <summary>
    /// Interface IClientRepository
    /// </summary>
    public interface IClientRepository
    {
        /// <summary>
        /// Gets the registered clients.
        /// </summary>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        Task<int> GetRegisteredClients();

        /// <summary>
        /// Saves the server usage.
        /// </summary>
        /// <param name="serverUsage">The client connection usage.</param>
        /// <returns>Task.</returns>
        Task SaveServerUsage(ServerUsage serverUsage);

        /// <summary>
        /// Gets the server usage.
        /// </summary>
        /// <returns>Task&lt;List&lt;ServerUsage&gt;&gt;.</returns>
        Task<List<ServerUsage>> GetServerUsage();
    }
}
