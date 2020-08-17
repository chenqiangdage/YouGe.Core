using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YouGe.Core.DBEntitys.Sys;
using YouGe.Core.Models.DTModel.Sys;
using YouGe.Core.Models.System;


namespace YouGe.Core.Interface.IRepositorys.Sys
{
    public interface ISysPermissionRepository : IRepository<SysRole, int>
    {
        /// <summary>
        /// 根据用户ID查询权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>权限列表</returns>
        public List<string> selectRolePermissionByUserId(long userId);
    }
}
