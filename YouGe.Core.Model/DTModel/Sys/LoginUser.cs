using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.DBEntitys.Sys;

namespace YouGe.Core.Models.DTModel.Sys
{
    public class LoginUser
    {
        public static readonly long serialVersionUID = 1L;
        /// <summary>
        /// 用户唯一标识
        /// <summary>
        public string  token { get; set; }

        /// <summary>
        /// 登陆时间
        /// <summary>
        public long loginTime { get; set; }


        /// <summary>
        /// 过期时间
        /// <summary>
        public long expireTime { get; set; }

        /// <summary>
        /// 登录IP地址
        /// <summary>
        public string ipaddr { get; set; }

        /// <summary>
        /// 登录地点
        /// <summary>
        public string loginLocation { get; set; }

        /// <summary>
        /// 浏览器类型
        /// <summary>
        public string browser { get; set; }

        /// <summary>
        /// 操作系统
        /// <summary>
        private string os { get; set; }

        /// <summary>
        /// 权限列表
        /// <summary>
        public List<string> permissions { get; set; }

        /// <summary>
        /// 用户信息
        /// <summary>
        public SysUser user { get; set; }


        public LoginUser()
        {
        }

        public LoginUser(SysUser user, List<string> permissions)
        {
            this.user = user;
            this.permissions = permissions;
        }

        
    public string getPassword()
    {
            return user.Password;
    }

     
    public string getUsername()
    {
            return user.UserName;
    }
        /// <summary>
        /// 账户是否未过期,过期无法验证
        /// </summary>
        /// <returns></returns>
        public bool isAccountNonExpired()
        {
            return true;
        }
        /// <summary>
        /// 指定用户是否解锁,锁定的用户无法进行身份验证
        /// </summary>
        /// <returns></returns>
        public bool isAccountNonLocked()
        {
            return true;
        }
        /// <summary>
        /// 指示是否已过期的用户的凭据(密码),过期的凭据防止认证
        /// </summary>
        /// <returns></returns>
        public bool isCredentialsNonExpired()
        {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool isEnabled()
        {
            return true;
        }
    }
}
