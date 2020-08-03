using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.Interface.IRepositorys.Sys;
using YouGe.Core.DBEntitys.Sys;
using YouGe.Core.Interface.IDbContexts;
using YouGe.Core.Models.DTModel.Sys;
using YouGe.Core.Common.YouGeException;
using YouGe.Core.Commons.Helper;
using YouGe.Core.Common.SystemConst;

namespace YouGe.Core.Repositorys.Sys
{
    public class SysLoginRepository : BaseRepository<SysLoginInfor, int>, ISysLoginRepository
    {
        public SysLoginRepository(IYouGeDbContext dbContext) : base(dbContext)
        {
        }

        public LoginUser loadUserByUsername(string username,string password)
        {
            SysUser user = userService.selectUserByUserName(username);
            if (user==null)
            {
                Log4NetHelper.Info(string.Format(" 登录用户：{0} 不存在.",username));             
                throw new UsernameNotFoundException("登录用户：" + username + " 不存在");
            }
            else if (UserStatus.DELETED.ToString()==user.DelFlag.ToString())
            {
                Log4NetHelper.Info(string.Format(" 登录用户：{0} 已被删除.", username));
            
                throw new BaseException("对不起，您的账号：" + username + " 已被删除");
            }
            else if (UserStatus.DISABLE.ToString()==user.status.ToString())
            {
                Log4NetHelper.Info(string.Format(" 登录用户：{0} 已被停用.", username));              
                throw new BaseException("对不起，您的账号：" + username + " 已停用");
            }

            return createLoginUser(user);
        }


        public LoginUser createLoginUser(SysUser user)
        {
            return new LoginUser(user, permissionService.getMenuPermission(user));
        }
        public void recordLogininfor(string userName, string status, string message)
        {
            throw new NotImplementedException();
        }
    }
}
