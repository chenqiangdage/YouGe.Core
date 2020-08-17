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
    public class SysPermissionService : ISysPermissionService
    {
        private ISysPermissionRepository sysPermissionRepository;
        private ISysMenuRepository sysMenuRepository;
        public SysPermissionService(ISysPermissionRepository SysPermissionRepository, ISysMenuRepository SysMenuRepository)
        {
            sysPermissionRepository = SysPermissionRepository;
            sysMenuRepository = SysMenuRepository;
        }

        /// <summary>
        /// 获取菜单数据权限
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>菜单权限信息</returns>
        public List<string> getMenuPermission(SysUser user)
        {
            List<string> perms = new List<string>();
            if(user.isAdmin())
            {
                perms.Add("*:*:*");
            }else
            {
                List<string> permissions = sysMenuRepository.selectMenuPermsByUserId(user.Id);
                perms.AddRange(permissions);
            }
            return perms;

        }

        /// <summary>
        /// 获取角色数据权限
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>角色权限信息</returns>      
        public List<string> getRolePermission(SysUser user)
        {
            List<string> roles = new List<string>();
            
            if (user.isAdmin())
            {
                roles.Add("admin");
            }
            else
            {
                List<string> permissions =  sysPermissionRepository.selectRolePermissionByUserId(user.Id);
                roles.AddRange(permissions);
            }
            return roles;
        }
    }
}
