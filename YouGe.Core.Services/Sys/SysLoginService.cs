using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using YouGe.Core.Common.Helper;
using YouGe.Core.Commons.SystemConst;
using YouGe.Core.Interface.IServices.Sys;

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
                AsyncManager.me().execute(AsyncFactory.recordLogininfor(username, Constants.LOGIN_FAIL, MessageUtils.message("user.jcaptcha.expire")));
                throw new CaptchaExpireException();
            }
            if (!code.equalsIgnoreCase(captcha))
            {
                AsyncManager.me().execute(AsyncFactory.recordLogininfor(username, Constants.LOGIN_FAIL, MessageUtils.message("user.jcaptcha.error")));
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
                if (e instanceof BadCredentialsException)
            {
                    AsyncManager.me().execute(AsyncFactory.recordLogininfor(username, Constants.LOGIN_FAIL, MessageUtils.message("user.password.not.match")));
                    throw new UserPasswordNotMatchException();
                }
            else
                {
                    AsyncManager.me().execute(AsyncFactory.recordLogininfor(username, Constants.LOGIN_FAIL, e.getMessage()));
                    throw new CustomException(e.getMessage());
                }
            }
            AsyncManager.me().execute(AsyncFactory.recordLogininfor(username, Constants.LOGIN_SUCCESS, MessageUtils.message("user.login.success")));
            LoginUser loginUser = (LoginUser)authentication.getPrincipal();
            // 生成token
            return tokenService.createToken(loginUser);
          
        }
    }
}
