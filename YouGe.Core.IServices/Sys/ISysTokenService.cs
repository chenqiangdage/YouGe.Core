using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.Models.DTModel.Sys;

namespace YouGe.Core.Interface.IServices.Sys
{
    /// <summary>
    /// token验证处理
    /// </summary>
    public interface ISysTokenService
    {
        /// <summary>
        /// 获取用户身份信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns>用户信息</returns>
        public LoginUser getLoginUser(HttpRequest request);

        /// <summary>
        /// 设置用户身份信息
        /// </summary>
        public void setLoginUser(LoginUser loginUser);

        /// <summary>
        /// 删除用户身份信息
        /// </summary>
        public void delLoginUser(string token);

        /// <summary>
        /// 创建令牌
        /// <param name="loginUser ">用户信息</param>
        /// <returns> 令牌</returns>
        public string createToken(LoginUser loginUser);

        /// <summary>
        /// 验证令牌有效期，相差不足20分钟，自动刷新缓存
        /// <param name="loginUser">登录用户</param>
        /// <returns> 令牌</returns>
        public void verifyToken(LoginUser loginUser);
        /// <summary>
        /// 刷新令牌有效期
        /// <param name="loginUser"></param> 登录信息
        public void refreshToken(LoginUser loginUser);
        /// <summary>
        /// 设置用户代理信息
        /// <param name="loginUser"></param> 登录信息
        public void setUserAgent(LoginUser loginUser);
        /// <summary>
        /// 从数据声明生成令牌
        /// <param name="claims"></param> 数据声明
        /// <returns> 令牌
        public  string createToken(Dictionary<string, object> claims);
        /// <summary>
        /// 从令牌中获取数据声明        
        /// <param name="token"></param> 令牌
        /// <returns> 数据声明</returns>
        public  object parseToken(string token);
        /// <summary>
        /// 从令牌中获取用户名        
        /// <param name="token"></param> 令牌
        /// <returns> 用户名</returns>
        public string getUsernameFromToken(string token);
        /// <summary>
        /// 获取请求token        
        /// <param name="request"></param>
        /// <returns> token</returns>    
        public  string getToken(HttpRequest request);
        public  string getTokenKey(string uuid);
    }
}
