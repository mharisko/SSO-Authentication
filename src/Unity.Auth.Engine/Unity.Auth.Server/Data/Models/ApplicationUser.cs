// <copyright file="ApplicationUser.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>

namespace Unity.Auth.Server.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Application User.
    /// </summary>
    /// <seealso cref="IdentityUser" />
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address for this user.
        /// </summary>
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public override string Email
        {
            get
            {
                return base.Email;
            }

            set
            {
                base.Email = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this user is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool UserEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is Permanently Deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this user is permanently deleted; otherwise, <c>false</c>.
        /// </value>
        public bool PermanentlyDeleted { get; set; }
    }
}
