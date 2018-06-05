// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : haris.md
// Created          : 11-21-2017
//
// Last Modified By : haris.md
// Last Modified On : 11-22-2017
// ***********************************************************************
// <copyright file="ConsentInputModel.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Class ConsentInputModel.
    /// </summary>
    public class ConsentInputModel
    {
        /// <summary>
        /// Gets or sets the button.
        /// </summary>
        /// <value>The button.</value>
        public string Button { get; set; }

        /// <summary>
        /// Gets or sets the scopes consented.
        /// </summary>
        /// <value>The scopes consented.</value>
        public IEnumerable<string> ScopesConsented { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remember consent].
        /// </summary>
        /// <value><c>true</c> if [remember consent]; otherwise, <c>false</c>.</value>
        public bool RememberConsent { get; set; }

        /// <summary>
        /// Gets or sets the return URL.
        /// </summary>
        /// <value>The return URL.</value>
        public string ReturnUrl { get; set; }
    }
}