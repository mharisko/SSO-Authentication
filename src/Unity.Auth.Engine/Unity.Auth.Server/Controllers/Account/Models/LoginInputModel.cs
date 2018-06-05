// <copyright file="LoginInputModel.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>

namespace Unity.Auth.Server.Controllers.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Login Input Model calss.
    /// </summary>
    public class LoginInputModel
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remember login].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [remember login]; otherwise, <c>false</c>.
        /// </value>
        public bool RememberLogin { get; set; }

        /// <summary>
        /// Gets or sets the return URL.
        /// </summary>
        /// <value>
        /// The return URL.
        /// </value>
        public string ReturnUrl { get; set; }
    }
}