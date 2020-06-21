using System;
using System.Collections.Generic;
using System.Linq;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;

namespace YouGe.Core.IdentityService
{
    public class InMemoryConfiguration
    {
        public static IConfiguration Configuration { get; set; }
        /// <summary>
        /// Define which APIs will use this IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(
                    "user_api",
                    "user_api1",
                      new List<string>(){"role" }
                    ),
                   new ApiResource("clientservice", "clientservice1"),
            };
        }

        /// <summary>
        /// Define which Apps will use thie IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                #region OAuth 2.0 Demo Client
                new Client()
                {
                    ClientId ="user_api",
                    AllowedGrantTypes = new List<string>()
                    {
                        GrantTypes.ResourceOwnerPassword.FirstOrDefault(),//Resource Owner Password模式
                       // GrantTypeConstants.ResourceWeixinOpen,
                    },
                    ClientSecrets = {new Secret("user_secret".Sha256()) },
                    AllowOfflineAccess = true,//如果要获取refresh_tokens ,必须把AllowOfflineAccess设置为true
                    AllowedScopes= {
                        "user_api",
                        "offline_access",
                    },
                    AccessTokenLifetime = 36000,
                },
                 new Client
                {
                    ClientId = "client1",
                     AllowedGrantTypes = new List<string>()
                    {
                        GrantTypes.ResourceOwnerPassword.FirstOrDefault(),//Resource Owner Password模式
                        //GrantTypeConstants.ResourceWeixinOpen,
                    },
                    ClientSecrets = new [] { new Secret("client1".Sha256()) },
                     AllowOfflineAccess = true,//如果要获取refresh_tokens ,必须把AllowOfflineAccess设置为true
                    AllowedScopes = new [] { "clientservice" },
                    AccessTokenLifetime =36000,
                },
                #endregion
            };
        }

        /// <summary>
        /// Define which uses will use this IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TestUser> GetUsers()
        {
            return new[]
            {
                new TestUser()
                {
                     SubjectId = "1",
                     Username = "test",
                     Password = "123456"
                },
                new TestUser
                {
                    SubjectId = "10002",
                    Username = "andy@hotmail.com",
                    Password = "andypassword"
                },
                new TestUser
                {
                    SubjectId = "10003",
                    Username = "leo@hotmail.com",
                    Password = "leopassword"
                }
            };
        }
    }
}
