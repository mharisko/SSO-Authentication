// ***********************************************************************
// Assembly         : Unity.Auth.Server
// Author           : haris.md
// Created          : 11-08-2017
//
// Last Modified By : haris.md
// Last Modified On : 11-22-2017
// ***********************************************************************
// <copyright file="Defaults.cs" company="Unity Auth Server">
// Copyright (c) Muhammed Haris K. All rights reserved.
// Licensed under the Trial License, Version 1.0-alpha. See LICENSE in the project root for license information.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Unity.Auth.Server.AppStartup
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using IdentityServer4;
    using IdentityServer4.Models;
    using IdentityServer4.Test;

    /// <summary>
    /// Class Defaults.
    /// </summary>
    public class Defaults
    {
        // scopes define the resources in your system

        /// <summary>
        /// Gets the identity resources.
        /// </summary>
        /// <returns>IEnumerable&lt;IdentityResource&gt;.</returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        /// <summary>
        /// Gets the API resources.
        /// </summary>
        /// <returns>IEnumerable&lt;ApiResource&gt;.</returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            var api = new ApiResource("api", "My API");
            api.Scopes.Add(new Scope("apiv1"));
            return new List<ApiResource>
            {
               api
            };
        }

        // clients want to access resources (aka scopes)

        /// <summary>
        /// Gets the clients.
        /// </summary>
        /// <param name="serverSettings">The server settings.</param>
        /// <returns>IEnumerable&lt;Client&gt;.</returns>
        public static IEnumerable<Client> GetClients(ServerSettings serverSettings)
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api","apiv1" }
                },

                 // resource owner password grant client
                new Client
                {
                    ClientId = "Unity.Auth.Server.Api",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    ClientSecrets =
                    {
                        new Secret("Unity.Auth.Server.Client".Sha256())
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api",
                        "apiv1"
                    },
                    ClientName = "Unity Auth Server Client",
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = {  serverSettings.HostAddress },
                    PostLogoutRedirectUris = { serverSettings.HostAddress },
                    FrontChannelLogoutUri = $"{serverSettings.HostAddress}/signout-idsrv", // for testing identityserver on localhost
                },


                // OpenID Connect hybrid flow and client credentials client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    RequireConsent = true,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = { $"{ serverSettings.HostAddress}/signin-oidc" },
                    PostLogoutRedirectUris = { $"{ serverSettings.HostAddress}/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api",
                        "apiv1"
                    },
                    AllowOfflineAccess = true
                }
            };
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns>List&lt;TestUser&gt;.</returns>
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "Alice@123",

                   Claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "Alice Smith"),
                        new Claim(ClaimTypes.GivenName, "Alice"),
                        new Claim(ClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim(ClaimTypes.Role, "Geek"),
                        new Claim("website", "http://alice.com"),
                        new Claim(ClaimTypes.StreetAddress, @"{ ""street_address"": ""One Hacker Way"", ""locality"": ""Heidelberg"", ""postal_code"": 69118, ""country"": ""Germany"" }", "json")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "Bobs@123",

                    Claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "Bob Smith"),
                        new Claim(ClaimTypes.GivenName, "Bob"),
                        new Claim(ClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(ClaimTypes.Role, "Developer"),
                        new Claim(ClaimTypes.Role, "Geek"),
                        new Claim("website", "http://bob.com"),
                        new Claim(ClaimTypes.StreetAddress, @"{ ""street_address"": ""One Hacker Way"", ""locality"": ""Heidelberg"", ""postal_code"": 69118, ""country"": ""Germany"" }", "json")
                    }
                }
            };
        }
    }
}
