// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using NSW.Data;

namespace NSW.Idp
{
	using NSW.Idp.Models;
    using System.Linq;

    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
				new IdentityResources.Email(),
				new IdentityResources.Phone(),
				new IdentityResources.Profile(),
                // new IdentityResource("Starter.Api", "The starter api project's scope", new[] { "Starter.Api" }),
            };


        public static IEnumerable<ApiScope> ApiScopes => 
            new List<ApiScope>
            {
                new ApiScope("NSW.ApiScope", "The NSW project's API scope")
				{
					UserClaims = { 
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityModel.JwtClaimTypes.Email,
                        IdentityModel.JwtClaimTypes.Role,
                        IdentityModel.JwtClaimTypes.PhoneNumber,
						CustomClaimType.PostalCode.ToString(), 
						CustomClaimType.LanguagePreference.ToString()}
				}
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "NSW.Api",
                    DisplayName = "the nsw solution api",
                    ApiSecrets = {
                        new Secret("apisecret".Sha256()),  // TODO: change for production
                    },
                    Scopes = {
						//IdentityServerConstants.StandardScopes.OpenId,
						//IdentityServerConstants.StandardScopes.Email,
						//IdentityServerConstants.StandardScopes.Phone,
						//IdentityServerConstants.StandardScopes.Profile,
						"NSW.ApiScope",
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
                    ClientId = "NSW.Bff",
                    ClientSecrets = {
                        new Secret("secret".Sha256()), 
                    },
                    //AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    AllowedGrantTypes = { "authorization_code", "refresh_token", "client_credentials" },
                    RequirePkce = true,
                    // where to redirect to after login
                    RedirectUris = { "https://localhost/signin-oidc", "https://bff:5005/signin-oidc", "https://localhost/loggedin", "https://localhost:5005/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost/signout-callback-oidc", "https://localhost/loggedout" },

                    // what information the application is allowed access to
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Email,
						IdentityServerConstants.StandardScopes.Profile,
						"NSW.ApiScope"
					},
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    AllowedCorsOrigins = { // TODO: change for production
                        "http://localhost",
						"https://localhost",
                        "http://bff:5004",
                        "https://bff:5005",
                        "http://api:5002",
                        "https://api:5003",
                        "https://idp:5007",
                        "http://idp:5006",
						"http://localhost:5002",
						"https://localhost:5003",
						"http://localhost:5004",
						"https://localhost:5005",
						"http://localhost:5006",
						"https://localhost:5007",

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