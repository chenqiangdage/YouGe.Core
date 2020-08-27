using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using YouGe.Core.Common.Helper;
using YouGe.Core.Common.Security;
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
    public class SysMenuService : ISysMenuService
    {
        private ISysPermissionRepository sysPermissionRepository;
        private ISysMenuRepository sysMenuRepository;
        public SysMenuService(ISysPermissionRepository SysPermissionRepository, ISysMenuRepository SysMenuRepository)
        {
            sysPermissionRepository = SysPermissionRepository;
            sysMenuRepository = SysMenuRepository;
        }
        /// <summary>
        /// 构建前端路由所需要的菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        public List<RouterVo> buildMenus(List<SysMenu> menus)
        {
            List<RouterVo> routers = new List<RouterVo>();
            foreach (SysMenu menu in menus)
            {
                RouterVo router = new RouterVo();
                router.hidden = "1".Equals(menu.Visible);// .setHidden("1".equals(menu.getVisible()));
                router.name = getRouteName(menu);
                router.path = getRouterPath(menu);
                router.component = getComponent(menu);
                router.meta = new MetaVo(menu.MenuName, menu.Icon);
                List<SysMenu> cMenus = menu.getChildren();
                if (cMenus!=null && cMenus.Count > 0 && YouGeUserConstants.TYPE_DIR.Equals(menu.MenuType))
                {
                    router.alwaysShow =true;
                    router.redirect = "noRedirect";
                    router.children = buildMenus(cMenus);
                }
                else if (isMeunFrame(menu))
                {

                    List<RouterVo> childrenList = new List<RouterVo>();


                    RouterVo children = new RouterVo();
                    children.path = menu.Path;
                    children.component =menu.Component;
                    children.name =  menu.Path.TitleToUpper();
                    children.meta =new MetaVo(menu.MenuName, menu.Icon);
                    childrenList.Add(children);
                    router.children = childrenList;
                }
                routers.Add(router);
            }
            return routers;
             
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
        /// <summary>
        /// 根据用户ID查询菜单树信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<SysMenu> selectMenuTreeByUserId(long userId)
        {
            List<SysMenu> menus = null;            
            if (userId != 0 && 1L == userId)
            {                 
                menus = sysMenuRepository.selectMenuTreeAll();
            }
            else
            {
                menus = sysMenuRepository.selectMenuTreeByUserId(userId);
            }
            return getChildPerms(menus, 0);
        }

        public int updateMenu(SysMenu menu)
        {
            throw new NotImplementedException();
        }

 
        /// <summary>
        /// 根据父节点的ID获取所有子节点
        /// </summary>
        /// <param name="list">分类表</param>
        /// <param name="parentId">传入的父节点ID</param>
        /// <returns></returns>
        ///
        public List<SysMenu> getChildPerms(List<SysMenu> list, int parentId)
        {
            List<SysMenu> returnList = new List<SysMenu>();
            foreach(SysMenu item in list)
            {
                
                // 一、根据传入的某个父节点ID,遍历该父节点的所有子节点
                if (item.ParentId == parentId)
                {
                    recursionFn(list, item);
                    returnList.Add(item);
                }
            }
            return returnList;
        }
        /// <summary>
        /// 递归列表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="t"></param>
        private void recursionFn(List<SysMenu> list, SysMenu t)
        {
            // 得到子节点列表
            List<SysMenu> childList = getChildList(list, t);
            t.setChildren(childList);
            foreach(SysMenu tChild in childList)
            {
                if (hasChild(list, tChild))
                {
                    // 判断是否有子节点
                    var it = childList.GetEnumerator();
             
                    while (it.MoveNext())
                    {
                        SysMenu n = (SysMenu)it.Current;
                        recursionFn(list, n);
                    }
                }
            }
        }
        /// <summary>
        /// 得到子节点列表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private List<SysMenu> getChildList(List<SysMenu> list, SysMenu t)
        {
            List<SysMenu> tlist = new List<SysMenu>();
            var  it = list.GetEnumerator();
            while (it.MoveNext())
            {
                SysMenu n = (SysMenu)it.Current;
                if (n.ParentId == t.Id)
                {
                    tlist.Add(n);
                }
            }
            return tlist;
        }
        /// <summary>
        /// 判断是否有子节点
        /// </summary>
        /// <param name="list"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private bool hasChild(List<SysMenu> list, SysMenu t)
        {
            return getChildList(list, t).Count > 0 ? true : false;
        }
        /// <summary>
        /// 获取路由名称
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public String getRouteName(SysMenu menu)
        {
            String routerName = menu.Path.TitleToUpper();
            // 非外链并且是一级目录（类型为目录）
            if (isMeunFrame(menu))
            {
                routerName = string.Empty;
            }
            return routerName;
        }
        /// <summary>
        /// 获取路由地址
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public String getRouterPath(SysMenu menu)
        {
            String routerPath = menu.Path;
            // 非外链并且是一级目录（类型为目录）
            if (0 == menu.ParentId && YouGeUserConstants.TYPE_DIR.Equals(menu.MenuType)
                    && YouGeUserConstants.NO_FRAME.Equals(menu.Isframe))
            {
                routerPath = "/" + menu.Path;
            }
            // 非外链并且是一级目录（类型为菜单）
            else if (isMeunFrame(menu))
            {
                routerPath = "/";
            }
            return routerPath;
        }
        /// <summary>
        /// 获取组件信息
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public String getComponent(SysMenu menu)
        {
            String component = YouGeUserConstants.LAYOUT;
            if (!string.IsNullOrEmpty(menu.Component) && !isMeunFrame(menu))
            {
                component = menu.Component;
            }
            return component;
        }
        /// <summary>
        /// 是否为菜单内部跳转
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public bool isMeunFrame(SysMenu menu)
        {
            return menu.ParentId == 0 && YouGeUserConstants.TYPE_MENU.Equals(menu.MenuType)
                    && menu.Isframe.Equals(YouGeUserConstants.NO_FRAME);
        }
    }
}
