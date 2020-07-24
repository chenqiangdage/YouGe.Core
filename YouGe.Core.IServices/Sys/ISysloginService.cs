using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Interface.IServices.Sys
{
   public  interface ISysloginService
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="pUsername">用户名</param>
        /// <param name="pPassword">密码</param>
        /// <param name="pCode">验证码</param>
        /// <param name="pUuid">唯一标识</param>
        /// <returns>token</returns>
        public string login(string pUsername, string pPassword, string pCode, string pUuid);
    }
}
