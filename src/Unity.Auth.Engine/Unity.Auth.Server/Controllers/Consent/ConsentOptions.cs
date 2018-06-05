// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : haris.md
// Created          : 11-20-2017
//
// Last Modified By : haris.md
// Last Modified On : 11-21-2017
// ***********************************************************************
// <copyright file="ConsentOptions.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Helpers
{
    /// <summary>
    /// Class ConsentOptions.
    /// </summary>
    public class ConsentOptions
    {
        /// <summary>
        /// The must choose one error message
        /// </summary>
        public static readonly string MustChooseOneErrorMessage = "You must pick at least one permission";

        /// <summary>
        /// The invalid selection error message
        /// </summary>
        public static readonly string InvalidSelectionErrorMessage = "Invalid selection";

        /// <summary>
        /// Gets or sets a value indicating whether [enable offline access].
        /// </summary>
        /// <value><c>true</c> if [enable offline access]; otherwise, <c>false</c>.</value>
        public static bool EnableOfflineAccess { get; set; } = true;

        /// <summary>
        /// Gets or sets the display name of the offline access.
        /// </summary>
        /// <value>The display name of the offline access.</value>
        public static string OfflineAccessDisplayName { get; set; } = "Offline Access";

        /// <summary>
        /// Gets or sets the offline access description.
        /// </summary>
        /// <value>The offline access description.</value>
        public static string OfflineAccessDescription { get; set; } = "Access to your applications and resources, even when you are offline";
    }
}
