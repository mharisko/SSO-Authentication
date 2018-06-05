// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : haris.md
// Created          : 12-11-2017
//
// Last Modified By : haris.md
// Last Modified On : 12-11-2017
// ***********************************************************************
// <copyright file="UserEditModel.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Controllers.Api.Models
{
    /// <summary>
    /// Class UserEditModel.
    /// </summary>
    public class UserEditModel
    {
        /// <summary>
        /// Gets or sets gets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>The phone number.</value>
        public string PhoneNumber { get; set; }
    }
}