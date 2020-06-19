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
    public class UserAuthService: IUserAuthService
    {
        //mock data <user,role>
        private   Dictionary<string, string> users = new Dictionary<string, string>
        {
            { "admin", "admin" },
            { "jonny", "user" },
            { "xhl", "user" },
            { "james", "system" }
        };       

        private readonly AppSettings _appSettings;
        public UserAuthService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }       
        /// <summary>
        /// 用户认证获取信息 token
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ClientUser Authenticate(string username, string password)
        {
            ClientUser user = new ClientUser();
            user.Id = 1;
            var claimsIdentity = new ClaimsIdentity(new[]{
            new Claim(ClaimTypes.Name,username)
            });
            KeyValuePair<string,string>?  tuser  = users.Where(u => u.Key == username).FirstOrDefault();
            if (tuser == null) return null;

                claimsIdentity.AddClaims(new[]
                {
                new Claim( ClaimTypes.Email, "xhl.jonny@gmail.com"),
                new Claim( "ManageId", "admin"),
                new Claim(ClaimTypes.Role,tuser.Value.Value)
                });
            
            var key = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);
            var handler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.Now.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var securityToken = handler.CreateToken(tokenDescriptor);
            var token = handler.WriteToken(securityToken);
            user.Token =token;
            return user;
        }
    }
}
