// <copyright file="LoggedOutViewModel.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>

namespace Unity.Auth.Server.Controllers.Models
{
    /// <summary>
    /// Logged Out View Model class.
    /// </summary>
    public class LoggedOutViewModel
    {
        /// <summary>
        /// Gets or sets the post logout redirect URI.
        /// </summary>
        /// <value>
        /// The post logout redirect URI.
        /// </value>
        public string PostLogoutRedirectUri { get; set; }

        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>
        /// The name of the client.
        /// </value>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the sign out iframe URL.
        /// </summary>
        /// <value>
        /// The sign out iframe URL.
        /// </value>
        public string SignOutIframeUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic redirect after sign out].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic redirect after sign out]; otherwise, <c>false</c>.
        /// </value>
        public bool AutomaticRedirectAfterSignOut { get; set; }

        /// <summary>
        /// Gets or sets the logout identifier.
        /// </summary>
        /// <value>
        /// The logout identifier.
        /// </value>
        public string LogoutId { get; set; }
    }
}