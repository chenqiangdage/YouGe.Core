using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using YouGe.Core.Common.Helper;
using YouGe.Core.Common.YouGeException;
using YouGe.Core.Commons.SystemConst;
using YouGe.Core.Interface.IRepositorys.Sys;
using YouGe.Core.Interface.IServices.Sys;
using YouGe.Core.Models.DTModel.Sys;

namespace YouGe.Core.Services.Sys
{
    public class SysLoginService : ISysLoginService
    {
        private  ISysTokenService tokenService;
        private ISysLoginRepository sysLoginRepository;
        public SysLoginService(ISysTokenService pTokenService, ISysLoginRepository pSysLoginRepository)
        {
            tokenService = pTokenService;
            sysLoginRepository = pSysLoginRepository;
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
                sysLoginRepository.recordLogininfor(username, SystemConst.LOGIN_FAIL, "验证码已失效")
                );
                ta.Start();
                 
                throw new CaptchaExpireException();
            }
            
            if (!string.Equals(code, captcha, StringComparison.OrdinalIgnoreCase))
            {
                var tb = new Task(() =>
                   sysLoginRepository.recordLogininfor(username, SystemConst.LOGIN_FAIL, "验证码已失效")
                   );
                tb.Start();             
                throw new CaptchaException();
            }         
            try
            {                 
                LoginUser loginUser =  sysLoginRepository.loadUserByUsername(username, password);
                var tf = new Task(() =>
                 sysLoginRepository.recordLogininfor(username, SystemConst.LOGIN_SUCCESS, "登录成功")
                  );
                tf.Start();
                // 生成token
                return tokenService.createToken(loginUser);
            }
            catch (Exception e)
            {
                if ( e.Message.Contains("密码错误"))
                {
                    var tc = new Task(() =>
                    sysLoginRepository.recordLogininfor(username, SystemConst.LOGIN_FAIL, "用户不存在/密码错误")
                    ) ;
                    tc.Start();

                   
                    throw new UserPasswordNotMatchException();
                }
                else
                {
                    var td = new Task(() =>
                   sysLoginRepository.recordLogininfor(username, SystemConst.LOGIN_FAIL, e.Message)
                   );
                    td.Start();
                    
                    throw new CustomException(e.Message);
                }
            }

             
            
          
          
        }
    }
}
