// <copyright file="GrantsViewModel.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>

namespace Unity.Auth.Server.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Class GrantsViewModel.
    /// </summary>
    public class GrantsViewModel
    {
        /// <summary>
        /// Gets or sets the grants.
        /// </summary>
        /// <value>The grants.</value>
        public IEnumerable<GrantViewModel> Grants { get; set; }
    }
}
