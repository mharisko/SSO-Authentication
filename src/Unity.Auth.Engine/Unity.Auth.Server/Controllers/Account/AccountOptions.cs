// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : haris.md
// Created          : 11-20-2017
//
// Last Modified By : haris.md
// Last Modified On : 12-12-2017
// ***********************************************************************
// <copyright file="AccountOptions.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Class AccountOptions.
    /// </summary>
    public class AccountOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether the show logout prompt
        /// </summary>
        /// <value><c>true</c> if [show logout prompt]; otherwise, <c>false</c>.</value>
        public static bool ShowLogoutPrompt { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the automatic redirect after sign out
        /// </summary>
        /// <value><c>true</c> if [automatic redirect after sign out]; otherwise, <c>false</c>.</value>
        public static bool AutomaticRedirectAfterSignOut { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the allow remember login
        /// </summary>
        /// <value><c>true</c> if [allow remember login]; otherwise, <c>false</c>.</value>
        public static bool AllowRememberLogin { get; set; } = false;
    }
}
