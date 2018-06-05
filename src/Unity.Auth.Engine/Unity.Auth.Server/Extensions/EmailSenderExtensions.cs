// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : Muhammed Haris K
// Created          : 12-11-2017
//
// Last Modified By : Muhammed Haris K
// Last Modified On : 12-11-2017
// ***********************************************************************
// <copyright file="EmailSenderExtensions.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.Extensions
{
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using Unity.Auth.Services;

    /// <summary>
    /// Class EmailSenderExtensions.
    /// </summary>
    public static class EmailSenderExtensions
    {
        /// <summary>
        /// Sends the email confirmation asynchronous.
        /// </summary>
        /// <param name="emailSender">The email sender.</param>
        /// <param name="email">The email.</param>
        /// <param name="link">The link.</param>
        /// <returns>Asynchronous operation.</returns>
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(new EmailModel
            {
                ToAddeess = email,
                Subject = "Confirm your email",
                Body = $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>"
            });
        }
    }
}
