// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : haris.md
// Created          : 11-08-2017
//
// Last Modified By : haris.md
// Last Modified On : 11-22-2017
// ***********************************************************************
// <copyright file="Startup.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.AppStartup
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using IdentityServer4.EntityFramework.DbContexts;
    using IdentityServer4.EntityFramework.Mappers;
    using IdentityServer4.Test;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Rewrite;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.PlatformAbstractions;
    using NLog;
    using NLog.Extensions.Logging;
    using NLog.Web;
    using Swashbuckle.AspNetCore.Swagger;
    using Unity.Auth.Server.Data.Models;
    using Unity.Auth.Server.Data.Storage;
    using Unity.Auth.Server.Filters;
    using Unity.Auth.Server.Repositories;
    using Unity.Auth.Server.Repositories.Base;
    using Unity.Auth.Services;

    /// <summary>
    /// Application Starup class.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            ServerSettings serverSettings = this.Configuration.GetSection("ServerSettings").Get<ServerSettings>();
            services.Configure<ServerSettings>(this.Configuration.GetSection("ServerSettings"));

            services.Configure<SmsSettings>(this.Configuration.GetSection("SmsSettings"));
            services.Configure<EmailSettings>(this.Configuration.GetSection("EmailSettings"));
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.AddScoped(typeof(ApplicationUserManager));
            services.AddScoped(typeof(ApplicationUserRoleManager));
            services.AddScoped<SessionManagerAttribute>();

            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(SessionManagerAttribute));
            });

            services.AddAntiforgery();

            this.ConfigureAuthServices(services, serverSettings);

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                     "v1",
#pragma warning disable SA1118 // Parameter must not span multiple lines
                    info: new Info
                    {
                        Version = "v1",
                        Title = "Unity Authentication Server API",
                        Description = "A simple example ASP.NET Core Web API",
                        TermsOfService = "None",
                        Contact = new Contact { Name = "Muhammed Haris K", Email = string.Empty, Url = string.Empty },
                    });
#pragma warning restore SA1118 // Parameter must not span multiple lines

                // Set the comments path for the Swagger JSON and UI.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Unity.Auth.Server.xml");
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The environment detail.</param>
        /// <param name="options">The options.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<ServerSettings> options, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();

            // add NLog.Web
            app.AddNLogWeb();

            // this will do the initial DB population, but we only need to do it once
            // this is just in here as a easy, yet hacky, way to get our DB created/populated
            InitializeDatabase(app, options.Value).Wait();

            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(minLevel: Microsoft.Extensions.Logging.LogLevel.Debug);
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                LogManager.Configuration.Variables["connectionString"] = this.Configuration.GetConnectionString("NLogDb");
                LogManager.Configuration.Variables["configDir"] = "C:\\AspNetCoreNlog\\Logs";
                app.UseExceptionHandler("/Home/Error");
            }

            var rewriteOptions = new RewriteOptions()
                                    .AddRedirectToHttps();

            app.UseRewriter(rewriteOptions);

            app.UseStaticFiles();

            // app.UseAuthentication(); // not needed, since UseIdentityServer adds the authentication middleware
            ConfigureAuth(app);

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Unity Authentication Server API");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // app.UseAuthenticationSessionHandler();
        }

        /// <summary>
        /// Initializes the database.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="serverSettings">The server settings.</param>
        /// <returns>Task.</returns>
        private async Task InitializeDatabase(IApplicationBuilder app, ServerSettings serverSettings)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var authContext = serviceScope.ServiceProvider.GetRequiredService<UnityAuthDbContext>();
                authContext.Database.Migrate();

                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Defaults.GetClients(serverSettings))
                    {
                        await context.Clients.AddAsync(client.ToEntity());
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Defaults.GetIdentityResources())
                    {
                        await context.IdentityResources.AddAsync(resource.ToEntity());
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Defaults.GetApiResources())
                    {
                        await context.ApiResources.AddAsync(resource.ToEntity());
                    }

                    await context.SaveChangesAsync();
                }

                if (!authContext.Users.Any())
                {
                    await ConfigureUsers(Defaults.GetUsers(), serviceScope);
                }
            }
        }

        /// <summary>
        /// Configures the users.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="serviceScope">The service scope.</param>
        /// <returns>Task.</returns>
        private async Task ConfigureUsers(IEnumerable<TestUser> users, IServiceScope serviceScope)
        {
            try
            {
                var userManager = serviceScope.ServiceProvider.GetRequiredService<ApplicationUserManager>();
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<ApplicationUserRoleManager>();

                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new ApplicationUserRole { Id = "adm", Name = "Admin" });
                }

                if (!await roleManager.RoleExistsAsync("Employee"))
                {
                    await roleManager.CreateAsync(new ApplicationUserRole { Id = "Employee", Name = "Employee" });
                }

                if (!await roleManager.RoleExistsAsync("Manager"))
                {
                    await roleManager.CreateAsync(new ApplicationUserRole { Id = "Manager", Name = "Manager" });
                }

                foreach (TestUser user in users)
                {
                    if (await userManager.FindByNameAsync(user.Username) == null)
                    {
                        ApplicationUser appUser = new ApplicationUser
                        {
                            UserName = user.Username,
                            Email = user.Claims.First(s => string.Compare(s.Type, System.Security.Claims.ClaimTypes.Email, true) == 0).Value,
                        };

                        var result = await userManager.CreateAsync(appUser, user.Password);
                        if (result.Succeeded)
                        {
                            var result1 = await userManager.AddToRoleAsync(appUser, "Admin");
                            foreach (System.Security.Claims.Claim claim in user.Claims)
                            {
                                await userManager.AddClaimAsync(appUser, claim);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }
    }
}
