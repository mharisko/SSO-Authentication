// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-20-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 12-12-2017
// ***********************************************************************
// <copyright file="OperationalServiceOptions.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Options
{
    /// <summary>
    /// Class OperationalServiceOptions.
    /// </summary>
    public class OperationalServiceOptions
    {
        /// <summary>
        /// Gets or sets the size of the token cleanup batch.
        /// </summary>
        /// <value>The size of the token cleanup batch.</value>
        public int TokenCleanupBatchSize { get; set; }

        /// <summary>
        /// Gets or sets the token cleanup interval.
        /// </summary>
        /// <value>The token cleanup interval.</value>
        public int TokenCleanupInterval { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable token cleanup].
        /// </summary>
        /// <value><c>true</c> if [enable token cleanup]; otherwise, <c>false</c>.</value>
        public bool EnableTokenCleanup { get; set; }
    }
}
