using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.Interface.IServices.Sys;
using YouGe.Core.Models.DTModel.Sys;
using YouGe.Core.Commons;
using YouGe.Core.Commons.SystemConst;
using YouGe.Core.Common.Helper;
using YouGe.Core.Common.Extensions;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using YouGe.Core.Commons.Helper;
using YouGe.Core.Models.System;

namespace YouGe.Core.Services.Sys
{
    public class SysTokenService : ISysTokenService
    {
        protected static readonly long MILLIS_SECOND = 1000;

        protected static readonly long MILLIS_MINUTE = 60 * MILLIS_SECOND;

        private static readonly long MILLIS_MINUTE_TEN = 20 * 60 * 1000L;
        public string createToken(LoginUser loginUser, RequestBasicInfo info)
        {
            // string token = IdUtils.fastUUID(); // TO DO 
            string token = Guid.NewGuid().ToString().Replace("-", "");
            loginUser.token = token;
            setUserAgent(loginUser,info);
            refreshToken(loginUser);
            var claims = new Claim[] {
                new Claim(YouGeSystemConst.LOGIN_USER_KEY, token)
            };             
            return createToken(claims);
        }

        public string createToken(Claim[] claims)
        {         
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456"));
            var token = new JwtSecurityToken(
                issuer: "YouGe.Core",
                audience: "YouGe.Core",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha512)
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
 
        }

        public void delLoginUser(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                string userKey = getTokenKey(token);
                YouGeRedisHelper.Del(userKey);
            }
        }

        public LoginUser getLoginUser(HttpRequest request)
        {
            // 获取请求携带的令牌
            string token = getToken(request);
            if (!string.IsNullOrEmpty(token))
            {
                List<Claim> claims = parseToken(token);
                // 解析对应的权限以及用户信息\
               string uuid =  claims.Where(U => U.Type == YouGeSystemConst.LOGIN_USER_KEY).FirstOrDefault().Value;
               // string uuid = (string)claims.get();
                string userKey = getTokenKey(uuid);
                LoginUser user = YouGeRedisHelper.Get<LoginUser>(userKey);
                return user;
            }
            return null;         
        }

        public string getToken(HttpRequest request)
        {
            string header = "Authorization"; // TO DO 这个要写在appsettiong.json文件中
            string token = request.Headers[header];
             
            if (!string.IsNullOrEmpty(token) && token.StartsWith(YouGeSystemConst.TOKEN_PREFIX))
            {
                token = token.Replace(YouGeSystemConst.TOKEN_PREFIX, "");
            }
            return token;
        }

        public string getTokenKey(string uuid)
        {
            return YouGeSystemConst.LOGIN_TOKEN_KEY + uuid;
        }

        public string getUsernameFromToken(string token)
        {           
            var claims = parseToken(token);
            return claims.Where(U => U.Type == ClaimTypes.Name).FirstOrDefault().Value;

        }

        public List<Claim> parseToken(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token);
            return jwtToken.Claims.ToList() ;
        }

        public void refreshToken(LoginUser loginUser)
        {
            loginUser.loginTime = DateTimeExtensions.CurrentTimeMillis();
            int expireTime = 30; // TO DO 这个要写在appsettiong.json文件中
            loginUser.expireTime = loginUser.loginTime + expireTime * MILLIS_MINUTE;
            // 根据uuid将loginUser缓存
            string  userKey = getTokenKey(loginUser.token);
            YouGeRedisHelper.Set(userKey, loginUser, expireTime * 60);             
        }

        public void setLoginUser(LoginUser loginUser)
        {
            if (loginUser!=null && !string.IsNullOrEmpty((loginUser.token)))
            {
                refreshToken(loginUser);
            }
        }

        public void setUserAgent(LoginUser loginUser, RequestBasicInfo info)
        {

           
            loginUser.ipaddr = info.Ip;
            loginUser.loginLocation = "TO DO ";
            loginUser.browser =  info.Device;
            loginUser.os = info.Os;
        }

        public void verifyToken(LoginUser loginUser)
        {
            long expireTime = loginUser.expireTime;
            long currentTime = DateTimeExtensions.CurrentTimeMillis();
            if (expireTime - currentTime <= MILLIS_MINUTE_TEN)
            {
                refreshToken(loginUser);
            }
        }
    }
}
