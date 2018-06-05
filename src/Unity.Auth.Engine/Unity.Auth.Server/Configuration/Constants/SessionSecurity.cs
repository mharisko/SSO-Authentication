// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : haris.md
// Created          : 11-20-2017
//
// Last Modified By : haris.md
// Last Modified On : 11-22-2017
// ***********************************************************************
// <copyright file="SessionSecurity.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Configuration.Constants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Class SessionSecurity.
    /// </summary>
    public static class SessionSecurity
    {
        /// <summary>
        /// The session expiry time
        /// </summary>
        public const int SessionExpiryTime = 30;

        /// <summary>
        /// The token cleanup interval
        /// </summary>
        public const int TokenCleanupInterval = 30;

        /// <summary>
        /// The token cleanup batch size
        /// </summary>
        public const int TokenCleanupBatchSize = 2;
    }
}
