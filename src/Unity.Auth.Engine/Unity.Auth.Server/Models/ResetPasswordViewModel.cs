// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 11-08-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 11-21-2017
// ***********************************************************************
// <copyright file="ResetPasswordViewModel.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Models
{
    /// <summary>
    /// Class ResetPasswordViewModel.
    /// </summary>
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; internal set; }

        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <value>The code.</value>
        public string Code { get; internal set; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; internal set; }
    }
}