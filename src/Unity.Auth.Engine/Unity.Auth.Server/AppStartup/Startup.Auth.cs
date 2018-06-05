// <copyright file="Startup.Auth.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>

namespace Unity.Auth.Server.AppStartup
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Reflection;
    using IdentityServer4.AccessTokenValidation;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Unity.Auth.Server.Configuration.Constants;
    using Unity.Auth.Server.Data.Models;
    using Unity.Auth.Server.Data.Storage;
    using Unity.Auth.Server.Identity;
    using Unity.Auth.Server.Options;
    using Unity.Auth.Services;

    public partial class Startup
    {
        public void ConfigureAuthServices(IServiceCollection services, ServerSettings serverSettings)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap = new Dictionary<string, string>();
            string connectionString = this.Configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<UnityAuthDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
            });

            services.AddIdentity<ApplicationUser, ApplicationUserRole>()
                .AddRoleManager<ApplicationUserRoleManager>()
                .AddUserManager<ApplicationUserManager>()
                .AddEntityFrameworkStores<UnityAuthDbContext>()
                .AddDefaultTokenProviders();

            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()

                // .AddClientStore<UnityClientStore>()
                .AddProfileService<UnityUserProfileService>()

                // .AddClientStoreCache<UnityClientStore>()
                .AddSigningCredential(IdentityServerBuilderExtensionsCrypto.CreateRsaSecurityKey())

                // this adds the config data from DB (clients, resources)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(
                            connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })

                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(
                            connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = SessionSecurity.TokenCleanupInterval;
                })
                .AddDefaultEndpoints()
                .AddRequiredPlatformServices()

                // .AddInMemoryPersistedGrants()
                // .AddInMemoryIdentityResources(Defaults.GetIdentityResources())
                // .AddInMemoryApiResources(Defaults.GetApiResources())
                // .AddInMemoryClients(Defaults.GetClients())
                .AddAspNetIdentity<ApplicationUser>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = serverSettings.HostAddress;
                    options.ApiName = "api";
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.ApiSecret = "secret";
                    options.SupportedTokens = SupportedTokens.Both;
                    options.JwtBearerEvents = new AuthJwtBearerEvents();
                });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromMinutes(SessionSecurity.SessionExpiryTime);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
            });

            // services
            //    .AddMvcCore(options =>
            //    {
            //        // require scope1 or scope2
            //        var policy = ScopePolicy.Create("apiv1");
            //        options.Filters.Add(new AuthorizeFilter(policy));
            //    })
            //    .AddJsonFormatters()
            //    .AddAuthorization();

            // services.AddAuthorization(options =>
            // {
            //    options.AddPolicy("apiv1Policy", builder =>
            //    {
            //        // require scope1
            //        builder.RequireScope("apiv1");
            //    });
            // });

            // add CORS policy for non-IdentityServer endpoints
            services.AddCors(options =>
            {
                options.AddPolicy("apiCorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            // Add application services.
            services.AddTransient<ISmsSender, SmsSender>();
            services.AddSingleton<TokenCleanup>();
            services.AddSingleton<IHostedService, TokenCleanupHost>();

            var storeOptions = new OperationalServiceOptions();
            services.AddSingleton(storeOptions);

            storeOptions.EnableTokenCleanup = true;
            storeOptions.TokenCleanupInterval = Convert.ToInt32(TimeSpan.FromMinutes(SessionSecurity.SessionExpiryTime).TotalSeconds);
            storeOptions.TokenCleanupBatchSize = SessionSecurity.TokenCleanupBatchSize;
        }

        /// <summary>
        /// Configures the authentication.
        /// </summary>
        /// <param name="app">The application.</param>
        public void ConfigureAuth(IApplicationBuilder app)
        {
            app.UseCors("apiCorsPolicy");
            app.UseIdentityServer();
        }
    }
}
