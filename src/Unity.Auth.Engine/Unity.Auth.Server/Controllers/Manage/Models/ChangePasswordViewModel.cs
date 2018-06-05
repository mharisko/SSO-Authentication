// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : haris.md
// Created          : 12-11-2017
//
// Last Modified By : haris.md
// Last Modified On : 12-11-2017
// ***********************************************************************
// <copyright file="ChangePasswordViewModel.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Controllers.Manage.Models
{
    /// <summary>
    /// Class ChangePasswordViewModel.
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// Gets or sets the status message.
        /// </summary>
        /// <value>The status message.</value>
        public string StatusMessage { get; set; }

        /// <summary>
        /// Gets the old password.
        /// </summary>
        /// <value>The old password.</value>
        public string OldPassword { get; internal set; }

        /// <summary>
        /// Gets the new password.
        /// </summary>
        /// <value>The new password.</value>
        public string NewPassword { get; internal set; }
    }
}