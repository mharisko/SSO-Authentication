// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-08-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 11-22-2017
// ***********************************************************************
// <copyright file="SendCodeViewModel.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// Class SendCodeViewModel.
    /// </summary>
    public class SendCodeViewModel
    {
        /// <summary>
        /// Gets or sets the providers.
        /// </summary>
        /// <value>The providers.</value>
        public List<SelectListItem> Providers { get; set; }

        /// <summary>
        /// Gets or sets the return URL.
        /// </summary>
        /// <value>The return URL.</value>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remember me].
        /// </summary>
        /// <value><c>true</c> if [remember me]; otherwise, <c>false</c>.</value>
        public bool RememberMe { get; set; }

        /// <summary>
        /// Gets or sets the selected provider.
        /// </summary>
        /// <value>The selected provider.</value>
        public string SelectedProvider { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SendCodeViewModel"/> is token.
        /// </summary>
        /// <value><c>true</c> if token; otherwise, <c>false</c>.</value>
        public bool Token { get; set; }
    }
}