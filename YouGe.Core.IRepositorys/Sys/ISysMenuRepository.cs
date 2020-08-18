using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.DBEntitys.Sys;

namespace YouGe.Core.Interface.IRepositorys.Sys
{
   public interface ISysMenuRepository : IRepository<SysMenu, int>
    {
   
        /// <summary>
        /// 根据用户ID查询权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>权限列表</returns>
        public List<string> selectMenuPermsByUserId(long userId);
    
        /// <summary>
        /// 根据用户ID查询菜单
        /// </summary>
        ///  
        /// <returns>菜单列表</returns>
        public List<SysMenu> selectMenuTreeAll();
     
        /// <summary>
        /// 根据用户ID查询菜单
        /// </summary>
        ///  <param name="userId">用户ID</param> 
        /// <returns>菜单列表</returns>
        public List<SysMenu> selectMenuTreeByUserId(long userId);


    }
}
