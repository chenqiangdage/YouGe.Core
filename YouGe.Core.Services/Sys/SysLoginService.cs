using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using YouGe.Core.Common.Helper;
using YouGe.Core.Common.YouGeException;
using YouGe.Core.Commons.SystemConst;
using YouGe.Core.Interface.IServices.Sys;
using YouGe.Core.Models.DTModel.Sys;

namespace YouGe.Core.Services.Sys
{
    public class SysLoginService : ISysloginService
    {
        public ISysTokenService tokenService;
        public SysLoginService(ISysTokenService _tokenService)
        {
            tokenService = _tokenService;
        }

        public string login(string username, string password, string code, string uuid)
        {
            string verifyKey = SystemConst.CAPTCHA_CODE_KEY + uuid;

            string captcha = YouGeRedisHelper.Get(verifyKey);
            YouGeRedisHelper.Del(verifyKey);             
            if (captcha == null)
            {
                //启动线程 记录日志
                var ta = new Task(() =>
                AsyncFactory.recordLogininfor(username, SystemConst.LOGIN_FAIL, "验证码已失效")
                );
                ta.Start();
                 
                throw new CaptchaExpireException();
            }
            
            if (!string.Equals(code, captcha, StringComparison.OrdinalIgnoreCase))
            {
                var tb = new Task(() =>
                   AsyncFactory.recordLogininfor(username, SystemConst.LOGIN_FAIL, "验证码已失效")
                   );
                tb.Start();             
                throw new CaptchaException();
            }
            // 用户验证
            Authentication authentication = null;
            try
            {
                // 该方法会去调用UserDetailsServiceImpl.loadUserByUsername
                authentication = authenticationManager
                        .authenticate(new UsernamePasswordAuthenticationToken(username, password));
            }
            catch (Exception e)
            {
                if (e.Message.Contains("密码错误"))
                {
                    var tc = new Task(() =>
                    AsyncFactory.recordLogininfor(username, SystemConst.LOGIN_FAIL, "用户不存在/密码错误")
                    ) ;
                    tc.Start();

                   
                    throw new UserPasswordNotMatchException();
                }
                else
                {
                    var td = new Task(() =>
                   AsyncFactory.recordLogininfor(username, SystemConst.LOGIN_FAIL, e.Message)
                   );
                    td.Start();
                    
                    throw new CustomException(e.Message);
                }
            }

                var tf = new Task(() =>
                    AsyncFactory.recordLogininfor(username, SystemConst.LOGIN_SUCCESS, "登录成功")
                     );
                tf.Start();
            
            LoginUser loginUser = (LoginUser)authentication.getPrincipal();
            // 生成token
            return tokenService.createToken(loginUser);
          
        }
    }
}
