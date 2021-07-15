using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IDS.Config
{
    public static class IDSConfiguration
    {
        public static List<IdentityResource> IdentityResources
        {
            get
            {
                List<IdentityResource> idResources = new List<IdentityResource>();
                idResources.Add(new IdentityResources.OpenId());
                idResources.Add(new IdentityResources.Profile());
                idResources.Add(new IdentityResources.Email());
                idResources.Add(new IdentityResources.Phone());
                idResources.Add(new IdentityResources.Address());
                idResources.Add(new IdentityResource("roles",
                    "User roles", new List<string> { "role" }));
                return idResources;
            }
        }
        public static List<ApiScope> ApiScopes
        {
            get
            {
                List<ApiScope> apiScopes = new List<ApiScope>();
                apiScopes.Add(new ApiScope("recipemakerapi", "RecipeMaker Web API"));
                return apiScopes;
            }
        }
        public static List<ApiResource> ApiResources
        {
            get
            {
                ApiResource apiResource1 = new ApiResource("recipemakerWebApiResource", "RecipeMaker Web API")
                {
                    Scopes = { "recipemakerapi" },
                    UserClaims = {
                        "role",
                        "given_name",
                        "family_name",
                        "email",
                        "phone",
                        "address"
                    }
                };

                List<ApiResource> apiResources = new List<ApiResource>();
                apiResources.Add(apiResource1);
                return apiResources;
            }
        }
        public static List<Client> Clients
        {
            get
            {
                Client client1 = new Client
                {
                    ClientId = "clientrecipemaker",
                    ClientName = "Client RecipeMaker",
                    ClientSecrets = new[] {
                        new Secret("clientrecipemaker_secret_code".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = {
                        "openid",
                        "roles",
                        "profile",
                        "recipemakerapi"
                    },
                    AllowOfflineAccess = true
                };

                Client client2 = new Client
                {
                    ClientId = "angularclientimplicit",
                    ClientName = "Angular Client Implicit",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:4200"
                    },

                    ClientSecrets =
                    {
                        new Secret("angularclientimplicitsecret".Sha256())
                    },
                    RedirectUris = { "https://localhost:4200/signin-callback" },
                    PostLogoutRedirectUris = { "https://localhost:4200/signout-callback" },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "recipemakerapi"
                    },
                    AllowAccessTokensViaBrowser = true,
                    AllowPlainTextPkce = true,
                    RequireConsent = false
                };

                List<Client> clients = new List<Client>();
                clients.Add(client1);
                clients.Add(client2);

                return clients;
            }
        }
        public static List<TestUser> TestUsers
        {
            get
            {
                TestUser usr1 = new TestUser()
                {
                    SubjectId = "2f47f8f0-bea1-4f0e-ade1-88533a0eaf57",
                    Username = "user1",
                    Password = "password1",
                    Claims = new List<Claim>
                        {
                            new Claim("given_name", "firstName1"),
                            new Claim("family_name", "lastName1"),
                            new Claim("address", "USA"),
                            new Claim("email","user1@localhost"),
                            new Claim("phone", "123"),
                            new Claim("role", "Admin")
                        }
                };

                TestUser usr2 = new TestUser()
                {
                    SubjectId = "5747df40-1bff-49ee-aadf-905bacb39a3a",
                    Username = "user2",
                    Password = "password2",
                    Claims = new List<Claim>
                        {
                            new Claim("given_name", "firstName2"),
                            new Claim("family_name", "lastName2"),
                            new Claim("address", "UK"),
                            new Claim("email","user2@localhost"),
                            new Claim("phone", "456"),
                            new Claim("role", "Operator")
                        }
                };

                TestUser usr3 = new TestUser()
                {
                    SubjectId = "18ac604d-deb7-40b2-bc3c-6b21f32d67b6",
                    Username = "user3",
                    Password = "password3",
                    Claims = new List<Claim>
                        {
                            new Claim("given_name", "firstName3"),
                            new Claim("family_name", "lastName3"),
                            new Claim("address", "UK"),
                            new Claim("email","user3@localhost"),
                            new Claim("phone", "789"),
                            new Claim("role", "User")
                        }
                };

                List<TestUser> testUsers = new List<TestUser>();
                testUsers.Add(usr1);
                testUsers.Add(usr2);
                testUsers.Add(usr3);

                return testUsers;
            }
        }
    }
}
