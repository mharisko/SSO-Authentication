// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : haris.md
// Created          : 11-21-2017
//
// Last Modified By : haris.md
// Last Modified On : 11-22-2017
// ***********************************************************************
// <copyright file="ConsentViewModel.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Class ConsentViewModel.
    /// </summary>
    /// <seealso cref="Unity.Auth.Server.Models.ConsentInputModel" />
    public class ConsentViewModel : ConsentInputModel
    {
        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>The name of the client.</value>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the client URL.
        /// </summary>
        /// <value>The client URL.</value>
        public string ClientUrl { get; set; }

        /// <summary>
        /// Gets or sets the client logo URL.
        /// </summary>
        /// <value>The client logo URL.</value>
        public string ClientLogoUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow remember consent].
        /// </summary>
        /// <value><c>true</c> if [allow remember consent]; otherwise, <c>false</c>.</value>
        public bool AllowRememberConsent { get; set; }

        /// <summary>
        /// Gets or sets the identity scopes.
        /// </summary>
        /// <value>The identity scopes.</value>
        public IEnumerable<ScopeViewModel> IdentityScopes { get; set; }

        /// <summary>
        /// Gets or sets the resource scopes.
        /// </summary>
        /// <value>The resource scopes.</value>
        public IEnumerable<ScopeViewModel> ResourceScopes { get; set; }
    }
}
