// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-08-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 11-22-2017
// ***********************************************************************
// <copyright file="UnityClientStore.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Data.Stores
{
    using System.Threading.Tasks;
    using IdentityServer4.Models;
    using IdentityServer4.Stores;

    /// <summary>
    /// Class UnityClientStore.
    /// </summary>
    /// <seealso cref="IdentityServer4.Stores.IClientStore" />
    public class UnityClientStore : IClientStore
    {
        /// <summary>
        /// Finds a client by id
        /// </summary>
        /// <param name="clientId">The client id</param>
        /// <returns>The client</returns>
        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return Task.FromResult(new Client());
        }
    }
}
