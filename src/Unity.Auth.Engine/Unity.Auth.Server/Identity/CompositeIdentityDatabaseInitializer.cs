// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-08-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 12-12-2017
// ***********************************************************************
// <copyright file="CompositeIdentityDatabaseInitializer.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Identity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    /// <summary>
    /// Class CompositeIdentityDatabaseInitializer.
    /// </summary>
    /// <seealso cref="Unity.Auth.Server.Identity.IIdentityDatabaseInitializer" />
    public class CompositeIdentityDatabaseInitializer : IIdentityDatabaseInitializer
    {
        /// <summary>
        /// The database initializers
        /// </summary>
        private readonly IEnumerable<IIdentityDatabaseInitializer> databaseInitializers;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeIdentityDatabaseInitializer"/> class.
        /// </summary>
        /// <param name="databaseInitializers">The database initializers.</param>
        public CompositeIdentityDatabaseInitializer(IEnumerable<IIdentityDatabaseInitializer> databaseInitializers)
        {
            this.databaseInitializers = databaseInitializers;
        }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; } = 0;

        /// <summary>
        /// seed as an asynchronous operation.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task SeedAsync()
        {
            foreach (var databaseInitializer in this.databaseInitializers.OrderBy(initializer => initializer.Order))
            {
                await databaseInitializer.SeedAsync();
            }
        }
    }
}
