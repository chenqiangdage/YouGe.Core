using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using YouGe.Core.Interface.IServices.IManager;
using YouGe.Core.Models.UsersInfo;
using YouGe.Core.Models.System;

namespace YouGe.Core.Services.Manager
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IDictionary<string, string> users = new Dictionary<string, string>
    {
        { "admin", "admin" },
        { "jonny", "jonny" },
        { "xhl", "xhl" },
        { "james", "james" }
    };
        public IDictionary<string, string> Tokens { get; } = new Dictionary<string, string>();

        public ClientUser Authenticate(string username, string password)
        {
            ClientUser cu = new ClientUser();
            cu.Id = 1212;
                 var claimsIdentity = new ClaimsIdentity(new[]{
            new Claim(ClaimTypes.Name,username)});
            if (!users.Any(u => u.Key == username && u.Value == password))
            {
                return null;
            }
            if (username == "admin")
            {
                claimsIdentity.AddClaims(new[]
                {
                new Claim( ClaimTypes.Email, "xhl.jonny@gmail.com"),
                new Claim( "ManageId", "admin"),
                new Claim(ClaimTypes.Role,"admin")
            });
            }
            var handler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.Now.AddMinutes(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretsecretsecret")), SecurityAlgorithms.HmacSha256),
            };
            var securityToken = handler.CreateToken(tokenDescriptor);
            var token = handler.WriteToken(securityToken);
            Tokens.Add(token, username);
            cu.Token = token;
            return cu;
        } 
        
    }
}
