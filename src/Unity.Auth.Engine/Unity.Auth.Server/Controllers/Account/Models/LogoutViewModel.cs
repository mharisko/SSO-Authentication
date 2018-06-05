// <copyright file="LogoutViewModel.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>

namespace Unity.Auth.Server.Controllers.Models
{
    /// <summary>
    /// Logout View Model class.
    /// </summary>
    /// <seealso cref="Unity.Auth.Server.Controllers.Models.LogoutInputModel" />
    public class LogoutViewModel : LogoutInputModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether [show logout prompt].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show logout prompt]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowLogoutPrompt { get; set; }
    }
}
