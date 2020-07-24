using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.DBEntitys.Sys;

namespace YouGe.Core.Interface.IServices.Sys
{
    public interface ISysMenuService
    {
        /// <summary>
        ///  根据用户查询系统菜单列表        
        /// <param name=" userId 用户ID
        /// <returns>菜单列表</returns>         
        public List<SysMenu> selectMenuList(long userId);

       /// <summary>
         /// 根据用户查询系统菜单列表          
        /// <param name=" menu 菜单信息
        /// <param name=" userId 用户ID
        /// <returns>菜单列表</returns>         
        public List<SysMenu> selectMenuList(SysMenu menu, long userId);

       /// <summary>
         /// 根据用户ID查询权限          
        /// <param name=" userId 用户ID
        /// <returns>权限列表</returns>         
        public List<string> selectMenuPermsByUserId(long userId);

       /// <summary>
         /// 根据用户ID查询菜单树信息          
        /// <param name=" userId 用户ID
        /// <returns>菜单列表</returns> 
        /// <returns></returns>
        public List<SysMenu> selectMenuTreeByUserId(long userId);

       /// <summary>
         /// 根据角色ID查询菜单树信息          
        /// <param name=" roleId 角色ID
        /// <returns>选中菜单列表</returns> 
        public List<int> selectMenuListByRoleId(long roleId);

       /// <summary>
         /// 构建前端路由所需要的菜单
        /// <param name=" menus 菜单列表
        /// <returns>路由列表</returns> 
        public List<RouterVo> buildMenus(List<SysMenu> menus);

       /// <summary>
         /// 构建前端所需要树结构
        /// <param name=" menus 菜单列表
        /// <returns>树结构列表</returns> 
        public List<SysMenu> buildMenuTree(List<SysMenu> menus);

       /// <summary>
         /// 构建前端所需要下拉树结构
        /// <param name=" menus 菜单列表
        /// <returns>下拉树结构列表</returns>         
        public List<TreeSelect> buildMenuTreeSelect(List<SysMenu> menus);

       /// <summary>
         /// 根据菜单ID查询信息
        /// <param name=" menuId 菜单ID
        /// <returns>菜单信息</returns> 
        public SysMenu selectMenuById(long menuId);

       /// <summary>
         /// 是否存在菜单子节点
        /// <param name=" menuId 菜单ID
        /// <returns>结果 true 存在 false 不存在</returns> 
        public bool hasChildByMenuId(long menuId);

       /// <summary>
         /// 查询菜单是否存在角色
        /// <param name=" menuId 菜单ID
        /// <returns>结果 true 存在 false 不存在</returns> 
        public bool checkMenuExistRole(long menuId);

       /// <summary>
         /// 新增保存菜单信息
        /// <param name=" menu 菜单信息
        /// <returns>结果</returns> 
        public int insertMenu(SysMenu menu);

       /// <summary>
         /// 修改保存菜单信息
        /// <param name=" menu 菜单信息
        /// <returns>结果</returns> 
        public int updateMenu(SysMenu menu);

       /// <summary>
         /// 删除菜单管理信息
        /// <param name=" menuId 菜单ID
        /// <returns>结果</returns> 
        public int deleteMenuById(long menuId);

       /// <summary>
         /// 校验菜单名称是否唯一
        /// <param name=" menu 菜单信息
        /// <returns>结果</returns>  
        public string checkMenuNameUnique(SysMenu menu);
    }
}
