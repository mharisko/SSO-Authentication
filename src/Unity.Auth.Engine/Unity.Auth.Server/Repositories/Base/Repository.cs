// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-20-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 11-21-2017
// ***********************************************************************
// <copyright file="Repository.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Repositories.Base
{
    using Data.Storage;

    /// <summary>
    /// Class Repository.
    /// </summary>
    /// <seealso cref="Unity.Auth.Server.Repositories.Base.IRepository" />
    public class Repository : IRepository
    {
        /// <summary>
        /// The unity authentication database context
        /// </summary>
        private UnityAuthDbContext unityAuthDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="unityAuthDbContext">The unity authentication database context.</param>
        public Repository(UnityAuthDbContext unityAuthDbContext)
        {
            this.unityAuthDbContext = unityAuthDbContext;
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>The database context.</value>
        protected virtual UnityAuthDbContext DbContext
        {
            get { return this.unityAuthDbContext; }
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposable"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposable)
        {
            if (disposable)
            {
                this.DbContext?.Dispose();
            }
        }
    }
}
