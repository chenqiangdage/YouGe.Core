using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.DBEntitys.Sys;
using YouGe.Core.Interface.IServices.Sys;
using YouGe.Core.Models.DTModel.Sys;

namespace YouGe.Core.Services.Sys
{
    public class SysMenuService : ISysMenuService
    {
        public List<RouterVo> buildMenus(List<SysMenu> menus)
        {
            throw new NotImplementedException();
        }

        public List<SysMenu> buildMenuTree(List<SysMenu> menus)
        {
            throw new NotImplementedException();
        }

        public List<TreeSelect> buildMenuTreeSelect(List<SysMenu> menus)
        {
            throw new NotImplementedException();
        }

        public bool checkMenuExistRole(long menuId)
        {
            throw new NotImplementedException();
        }

        public string checkMenuNameUnique(SysMenu menu)
        {
            throw new NotImplementedException();
        }

        public int deleteMenuById(long menuId)
        {
            throw new NotImplementedException();
        }

        public bool hasChildByMenuId(long menuId)
        {
            throw new NotImplementedException();
        }

        public int insertMenu(SysMenu menu)
        {
            throw new NotImplementedException();
        }

        public SysMenu selectMenuById(long menuId)
        {
            throw new NotImplementedException();
        }

        public List<SysMenu> selectMenuList(long userId)
        {
            throw new NotImplementedException();
        }

        public List<SysMenu> selectMenuList(SysMenu menu, long userId)
        {
            throw new NotImplementedException();
        }

        public List<int> selectMenuListByRoleId(long roleId)
        {
            throw new NotImplementedException();
        }

        public List<string> selectMenuPermsByUserId(long userId)
        {
            throw new NotImplementedException();
        }

        public List<SysMenu> selectMenuTreeByUserId(long userId)
        {
            throw new NotImplementedException();
        }

        public int updateMenu(SysMenu menu)
        {
            throw new NotImplementedException();
        }
    }
}
