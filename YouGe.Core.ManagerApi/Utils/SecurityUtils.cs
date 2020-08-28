using System;
using YouGe.Core.Common.Security;
using YouGe.Core.Common.SystemConst;
using YouGe.Core.Common.YouGeException;
using YouGe.Core.Models.DTModel.Sys;

namespace YouGe.Core.ManagerApi.Security
{
    public class SecurityUtils
    {
        public SecurityUtils()
        {
        }

        /**
    * 获取用户账户
    **/
        public static string getUsername()
        {
            try
            {
                return getLoginUser().getUsername();
            }
            catch (Exception e)
            {
                throw new CustomException("获取用户账户异常", HttpStatusConst.UNAUTHORIZED);
            }
        }

        /**
         * 获取用户
         **/
        public static LoginUser getLoginUser()
        {
            try
            {
                return (LoginUser)getAuthentication().getPrincipal();
            }
            catch (Exception e)
            {
                throw new CustomException("获取用户信息异常", HttpStatusConst.UNAUTHORIZED);
            }
        }

        /**
         * 获取Authentication
         */
        public static Authentication getAuthentication()
        {
            return SecurityContextHolder.getContext().getAuthentication();
        }

        /**
         * 生成BCryptPasswordEncoder密码
         *
         * @param password 密码
         * @return 加密字符串
         */
        public static string encryptPassword(string password)
        {

            return EncryptPassWord.EncryptPwd(password, "chenqiang");
             
        }

        /**
         * 判断密码是否相同
         *
         * @param rawPassword 真实密码
         * @param encodedPassword 加密后字符
         * @return 结果
         */
        public static bool matchesPassword(string rawPassword, string encodedPassword)
        {
           string realPassword =  EncryptPassWord.EncryptPwd(rawPassword, "chenqiang");
            return realPassword == encodedPassword;
        }


        public static bool isAdmin(long userId)
        {
            return userId != 0 && 1L == userId;
        }
    }
}
