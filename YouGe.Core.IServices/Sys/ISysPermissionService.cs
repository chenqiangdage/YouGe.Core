using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.DBEntitys.Sys;

namespace YouGe.Core.Interface.IServices.Sys
{
   public  interface ISysPermissionService
    {
        /// <summary>
        /// 获取角色数据权限
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>角色权限信息</returns>
        public List<string> getRolePermission(SysUser user);
        /// <summary>
        /// 获取菜单数据权限
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>菜单权限信息</returns>
        public List<string> getMenuPermission(SysUser user);
    }
}
