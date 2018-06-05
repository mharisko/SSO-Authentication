// <copyright file="LogoutInputModel.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>

namespace Unity.Auth.Server.Controllers.Models
{
    /// <summary>
    /// Logout Input Model class.
    /// </summary>
    public class LogoutInputModel
    {
        /// <summary>
        /// Gets or sets the logout identifier.
        /// </summary>
        /// <value>
        /// The logout identifier.
        /// </value>
        public string LogoutId { get; set; }
    }
}
