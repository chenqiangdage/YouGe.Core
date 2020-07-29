using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Models.DTModel.Sys
{
   public  class LoginModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public  string username { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        ///唯一标识 
        /// </summary>
        public string  uuid { get; set; }
    }
}
