// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : haris.md
// Created          : 11-20-2017
//
// Last Modified By : haris.md
// Last Modified On : 12-12-2017
// ***********************************************************************
// <copyright file="ProcessConsentResult.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Helpers
{
    using Unity.Auth.Server.Models;

    /// <summary>
    /// Class ProcessConsentResult.
    /// </summary>
    public class ProcessConsentResult
    {
        /// <summary>
        /// Gets a value indicating whether this instance is redirect.
        /// </summary>
        /// <value><c>true</c> if this instance is redirect; otherwise, <c>false</c>.</value>
        public bool IsRedirect => this.RedirectUri != null;

        /// <summary>
        /// Gets or sets the redirect URI.
        /// </summary>
        /// <value>The redirect URI.</value>
        public string RedirectUri { get; set; }

        /// <summary>
        /// Gets a value indicating whether [show view].
        /// </summary>
        /// <value><c>true</c> if [show view]; otherwise, <c>false</c>.</value>
        public bool ShowView => this.ViewModel != null;

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public ConsentViewModel ViewModel { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has validation error.
        /// </summary>
        /// <value><c>true</c> if this instance has validation error; otherwise, <c>false</c>.</value>
        public bool HasValidationError => this.ValidationError != null;

        /// <summary>
        /// Gets or sets the validation error.
        /// </summary>
        /// <value>The validation error.</value>
        public string ValidationError { get; set; }
    }
}
