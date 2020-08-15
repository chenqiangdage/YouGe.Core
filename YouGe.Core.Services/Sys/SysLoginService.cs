using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using YouGe.Core.Common.Helper;
using YouGe.Core.Common.SystemConst;
using YouGe.Core.Common.YouGeException;
using YouGe.Core.Commons;
using YouGe.Core.Commons.Helper;
using YouGe.Core.DBEntitys.Sys;
using YouGe.Core.Interface.IRepositorys.Sys;
using YouGe.Core.Interface.IServices.Sys;
using YouGe.Core.Models.DTModel.Sys;
using YouGe.Core.Models.System;

namespace YouGe.Core.Services.Sys
{
    public class SysLoginService : ISysLoginService
    {
        private  ISysTokenService tokenService;
        private ISysPermissionService permissionservice;
        private ISysLoginRepository sysLoginRepository;
        public ISysUserRepository sysUserRepository;
        public SysLoginService(ISysTokenService pTokenService, ISysPermissionService pPermissionservice ,ISysLoginRepository pSysLoginRepository, ISysUserRepository _sysUserRepository)
        {
            tokenService = pTokenService;
            permissionservice = pPermissionservice;
            sysLoginRepository = pSysLoginRepository;
            sysUserRepository = _sysUserRepository;
        }

        public string login(string username, string password, string code, string uuid, RequestBasicInfo info)
        {
            string verifyKey = YouGeSystemConst.CAPTCHA_CODE_KEY + uuid;

            string captcha = YouGeRedisHelper.Get(verifyKey);
            YouGeRedisHelper.Del(verifyKey);             
            if (captcha == null)
            {
                //启动线程 记录日志
                 var ta = new Task(() =>

                sysLoginRepository.recordLogininfor(username, YouGeSystemConst.FAIL, "没有验证码", info)
                );
                ta.Start();
                 
                throw new CaptchaExpireException();
            }
            
            if (!string.Equals(code, captcha, StringComparison.OrdinalIgnoreCase))
            {
                 var tb = new Task(() =>
                sysLoginRepository.recordLogininfor(username, YouGeSystemConst.FAIL, "验证码已失效", info)
                   );
                tb.Start();             
                throw new CaptchaException();
            }         
            try
            {                 
                LoginUser loginUser =  this.loadUserByUsername(username, password);
                   var tf = new Task(() =>
                sysLoginRepository.recordLogininfor(username, YouGeSystemConst.SUCCESS, "登录成功", info)
                  );
                tf.Start();
                // 生成token
                return tokenService.createToken(loginUser, info);
            }
            catch (Exception e)
            {
                if ( e.Message.Contains("密码错误"))
                {
                    var tc = new Task(() =>
                    sysLoginRepository.recordLogininfor(username, YouGeSystemConst.FAIL, "用户不存在/密码错误", info)
                    ) ;
                    tc.Start();

                   
                    throw new UserPasswordNotMatchException();
                }
                else
                {
                      var td = new Task(() =>
                    sysLoginRepository.recordLogininfor(username, YouGeSystemConst.FAIL, e.Message, info)
                   );
                    td.Start();
                    
                    throw new CustomException(e.Message);
                }
            }

             
            
          
          
        }

        private   LoginUser loadUserByUsername(string username, string password)
        {
            SysUser user = sysUserRepository.selectUserByUserName(username, password);
            if (user == null)
            {
                Log4NetHelper.Info(string.Format(" 登录用户：{0} 不存在.", username));
                throw new UsernameNotFoundException("登录用户：" + username + " 不存在");
            }
            else if (UserStatus.DELETED.ToString() == user.DelFlag.ToString())
            {
                Log4NetHelper.Info(string.Format(" 登录用户：{0} 已被删除.", username));

                throw new BaseException("对不起，您的账号：" + username + " 已被删除");
            }
            else if (UserStatus.DISABLE.ToString() == user.status.ToString())
            {
                Log4NetHelper.Info(string.Format(" 登录用户：{0} 已被停用.", username));
                throw new BaseException("对不起，您的账号：" + username + " 已停用");
            }

            return createLoginUser(user);
        }

        public LoginUser createLoginUser(SysUser user)
        {
            return new LoginUser(user, permissionservice.getMenuPermission(user));
        }

    }
}
