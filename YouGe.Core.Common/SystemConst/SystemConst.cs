using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Commons.SystemConst
{
    public  class SystemConst
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        public static readonly string OPERATE_SUCESS = "操作成功";
        /// <summary>
        /// 操作失败
        /// </summary>
        public static readonly string OPERATE_FAIL = "操作失败";

        public static readonly string UTF8 = "UTF-8";

        /// <summary>
        ///  GBK 字符集
        /// <summary>
        public static readonly String GBK = "GBK";

        /// <summary>
        ///  http请求
        /// <summary>
        public static readonly string HTTP = "http://";

        /// <summary>
        ///  https请求
        /// <summary>
        public static readonly string HTTPS = "https://";

        /// <summary>
        ///  通用成功标识
        /// <summary>
        public static readonly char SUCCESS = '0';

        /// <summary>
        ///  通用失败标识
        /// <summary>
        public static readonly char FAIL = '1';

        /// <summary>
        ///  登录成功
        /// <summary>
        public static readonly string LOGIN_SUCCESS = "Success";

        /// <summary>
        ///  注销
        /// <summary>
        public static readonly string LOGOUT = "Logout";

        /// <summary>
        ///  登录失败
        /// <summary>
        public static readonly string LOGIN_FAIL = "Error";

        /// <summary>
        ///  验证码 redis key
        /// <summary>
        public static readonly string CAPTCHA_CODE_KEY = "captcha_codes:";

        /// <summary>
        ///  登录用户 redis key
        /// <summary>
        public static readonly string LOGIN_TOKEN_KEY = "login_tokens:";

        /// <summary>
        ///  验证码有效期（分钟）
        /// <summary>
        public static readonly int CAPTCHA_EXPIRATION = 2;

        /// <summary>
        ///  令牌
        /// <summary>
        public static readonly string TOKEN = "token";

        /// <summary>
        ///  令牌前缀
        /// <summary>
        public static readonly string TOKEN_PREFIX = "Bearer ";

        /// <summary>
        ///  令牌前缀
        /// <summary>
        public static readonly string LOGIN_USER_KEY = "login_user_key";

        /// <summary>
        ///  用户ID
        /// <summary>
        public static readonly string JWT_USERID = "userid";

        /// <summary>
        ///  用户名称
        /// <summary>
        public static readonly string JWT_USERNAME = "sub";

        /// <summary>
        ///  用户头像
        /// <summary>
        public static readonly string JWT_AVATAR = "avatar";

        /// <summary>
        ///  创建时间
        /// <summary>
        public static readonly string JWT_CREATED = "created";

        /// <summary>
        ///  用户权限
        /// <summary>
        public static readonly string JWT_AUTHORITIES = "authorities";

        /// <summary>
        ///  参数管理 cache key
        /// <summary>
        public static readonly string SYS_CONFIG_KEY = "sys_config:";

        /// <summary>
        ///  字典管理 cache key
        /// <summary>
        public static readonly string SYS_DICT_KEY = "sys_dict:";

        /// <summary>
        ///  资源映射路径 前缀
        /// <summary>
        public static readonly string RESOURCE_PREFIX = "/profile";

        public static readonly string TODO = "TO DO";
    }
    /// <summary>
    /// 费用类型常量
    /// </summary>
    public class ConstFeeType
    {
        /// <summary>
        /// 人民币
        /// </summary>
        public static string CNY = "CNY";
    }

     
}
