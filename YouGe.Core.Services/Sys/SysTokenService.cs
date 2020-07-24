using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.Interface.IServices.Sys;
using YouGe.Core.Models.DTModel.Sys;
using YouGe.Core.Commons;
using YouGe.Core.Commons.SystemConst;
using YouGe.Core.Common.Helper;

namespace YouGe.Core.Services.Sys
{
    public class SysTokenService : ISysTokenService
    {
        protected static readonly long MILLIS_SECOND = 1000;

        protected static readonly long MILLIS_MINUTE = 60 * MILLIS_SECOND;

        private static readonly long MILLIS_MINUTE_TEN = 20 * 60 * 1000L;
        public string createToken(LoginUser loginUser)
        {
            // string token = IdUtils.fastUUID();
            string token = Guid.NewGuid().ToString().Replace("-","");
            loginUser.token =  token;
            setUserAgent(loginUser);
            refreshToken(loginUser);
            Dictionary<string, object> claims = new Dictionary<string, object>();
            claims.Add(SystemConst.LOGIN_USER_KEY, token);
            return createToken(claims);
        }

        public string createToken(Dictionary<string, object> claims)
        {
            throw new NotImplementedException();
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
                Claims claims = parseToken(token);
                // 解析对应的权限以及用户信息
                string uuid = (string)claims.get(SystemConst.LOGIN_USER_KEY);
                string userKey = getTokenKey(uuid);
                LoginUser user = YouGeRedisHelper.Get<LoginUser>(userKey);
                return user;
            }
            return null;
            throw new NotImplementedException();
        }

        public string getToken(HttpRequest request)
        {
            throw new NotImplementedException();
        }

        public string getTokenKey(string uuid)
        {
            throw new NotImplementedException();
        }

        public string getUsernameFromToken(string token)
        {
            throw new NotImplementedException();
        }

        public object parseToken(string token)
        {
            throw new NotImplementedException();
        }

        public void refreshToken(LoginUser loginUser)
        {
            throw new NotImplementedException();
        }

        public void setLoginUser(LoginUser loginUser)
        {
            if (loginUser!=null && !string.IsNullOrEmpty((loginUser.token))
            {
                refreshToken(loginUser);
            }
        }

        public void setUserAgent(LoginUser loginUser)
        {
            UserAgent userAgent = UserAgent.parseUserAgentString(ServletUtils.getRequest().getHeader("User-Agent"));
            String ip = IpUtils.getIpAddr(ServletUtils.getRequest());
            loginUser.setIpaddr(ip);
            loginUser.setLoginLocation(AddressUtils.getRealAddressByIP(ip));
            loginUser.setBrowser(userAgent.getBrowser().getName());
            loginUser.setOs(userAgent.getOperatingSystem().getName());
        }

        public void verifyToken(LoginUser loginUser)
        {
            long expireTime = loginUser.expireTime;
            long currentTime = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;            
            if (expireTime - currentTime <= MILLIS_MINUTE_TEN)
            {
                refreshToken(loginUser);
            }
        }
    }
}
