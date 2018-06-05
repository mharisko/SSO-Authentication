// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-20-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 11-21-2017
// ***********************************************************************
// <copyright file="ClientRepository.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using IdentityServer4.EntityFramework.DbContexts;
    using IdentityServer4.Stores;
    using Microsoft.EntityFrameworkCore;
    using Unity.Auth.Server.Data.Storage;
    using Unity.Auth.Server.Identity.Models;
    using Unity.Auth.Server.Repositories.Base;

    /// <summary>
    /// Class ClientRepository.
    /// </summary>
    /// <seealso cref="Repository" />
    /// <seealso cref="IClientRepository" />
    public class ClientRepository : Repository, IClientRepository
    {
        /// <summary>
        /// The configuration database context
        /// </summary>
        private readonly ConfigurationDbContext configurationDbContext;

        /// <summary>
        /// The client store
        /// </summary>
        private readonly IClientStore clientStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientRepository" /> class.
        /// </summary>
        /// <param name="unityAuthDbContext">The unity authentication database context.</param>
        /// <param name="configurationDbContext">The configuration database context.</param>
        /// <param name="clientStore">The client store.</param>
        public ClientRepository(UnityAuthDbContext unityAuthDbContext, ConfigurationDbContext configurationDbContext, IClientStore clientStore)
            : base(unityAuthDbContext)
        {
            this.configurationDbContext = configurationDbContext;
            this.clientStore = clientStore;
        }

        /// <summary>
        /// Finds the registered clients.
        /// </summary>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        public Task<int> GetRegisteredClients()
        {
            return this.configurationDbContext.Clients.CountAsync();
        }

        /// <summary>
        /// Gets the server usage.
        /// </summary>
        /// <returns>Task&lt;List&lt;ServerUsage&gt;&gt;.</returns>
        public Task<List<ServerUsage>> GetServerUsage()
        {
            return this.DbContext.ServerUsages.ToListAsync();
        }

        /// <summary>
        /// Saves the server usage.
        /// </summary>
        /// <param name="serverUsage">The client connection usage.</param>
        /// <returns>Task.</returns>
        public async Task SaveServerUsage(ServerUsage serverUsage)
        {
            await this.DbContext.ServerUsages.AddAsync(serverUsage);
            await this.DbContext.SaveChangesAsync();
        }
    }
}
