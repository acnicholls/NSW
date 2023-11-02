// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Starter.Idp
{
    using Starter.Idp.Models;
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                // new IdentityResource("Starter.Api", "The starter api project's scope", new[] { "Starter.Api" }),
            };


        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("Starter.Api", "The starter api project's scope")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "starter.api",
                    DisplayName = "the starter solution api",
                    ApiSecrets = {
                        new Secret("secret".Sha256()),
                    },
                    Scopes = {
                        "Starter.Api",
                    }
                },
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // new Client
                // {
                //     ClientId = "starter.app",
                //     ClientSecrets = { new Secret("secret".Sha256())},
                //     AllowedGrantTypes = GrantTypes.Code,
                //                         // where to redirect to after login
                //     RedirectUris = { "http://localhost/signin-oidc" },

                //     // where to redirect to after logout
                //     PostLogoutRedirectUris = { "http://localhost/signout-callback-oidc" },

                //     AllowedScopes = new List<string>
                //     {
                //         IdentityServerConstants.StandardScopes.OpenId,
                //         IdentityServerConstants.StandardScopes.Profile,
                //         "api1"
                //     },
                     
                // },
                new Client
                {
                    ClientId = "Starter.Bff",
                    ClientSecrets = {
                        new Secret("secret".Sha256()),
                    },
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RequirePkce = true,
                    // where to redirect to after login
                    RedirectUris = { "https://localhost/signin-oidc", "https://bff:5005/signin-oidc", "https://localhost/loggedin" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost/signout-callback-oidc", "https://localhost/loggedout" },                    

                    // what information the application is allowed access to
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "Starter.Api"
                    },
                    AllowOfflineAccess = true,
                    AllowedCorsOrigins = {
                        "https://localhost",
                        "http://bff:5004",
                        "https://bff:5005",
                        "http://api:5002",
                        "https://api:5003",
                        "https://idp:5007",
                        "http://idp:5006"
                    }
                },
            };

        public static List<ApplicationUser> TestUsers =>
            new List<ApplicationUser>{
                new ApplicationUser
                {

                },
            };
    }
}