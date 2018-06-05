// <copyright file="LoginViewModel.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>

namespace Unity.Auth.Server.Controllers.Models
{
    /// <summary>
    /// Login View Model class.
    /// </summary>
    /// <seealso cref="Unity.Auth.Server.Controllers.Models.LoginInputModel" />
    public class LoginViewModel : LoginInputModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether [allow remember login].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow remember login]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowRememberLogin { get; set; }
    }
}