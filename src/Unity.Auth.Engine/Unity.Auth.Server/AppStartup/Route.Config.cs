// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : haris.md
// Created          : 11-08-2017
//
// Last Modified By : haris.md
// Last Modified On : 11-21-2017
// ***********************************************************************
// <copyright file="Route.Config.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************


namespace Unity.Auth.Server.AppStartup
{
    using Microsoft.AspNetCore.Builder;

    /// <summary>
    /// Class Route.
    /// </summary>
    public class Route
    {
        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Config(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}