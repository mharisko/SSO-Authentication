// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-08-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 11-22-2017
// ***********************************************************************
// <copyright file="ApplicationUserStore.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Data.Storage
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    /// <summary>
    /// Class ApplicationUserStore.
    /// </summary>
    /// <seealso cref="UserStore" />
    public class ApplicationUserStore : UserStore
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserStore"/> class.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        public ApplicationUserStore(UnityAuthDbContext ctx)
            : base(ctx)
        {
        }
    }
}
