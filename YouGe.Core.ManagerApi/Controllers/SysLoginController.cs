using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YouGe.Core.Interface.IServices.IManager;
using YouGe.Core.Models.System;
using YouGe.Core.Models.DTModel.Sys;
using YouGe.Core.Commons.SystemConst;
using YouGe.Core.Interface.IServices.Sys;
using YouGe.Core.DBEntitys.Sys;

namespace YouGe.Core.ManagerApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SysLoginController : ControllerBase
    {

        private readonly ILogger<SysLoginController> logger;        
        private readonly ISysTokenService tokenService;
        private readonly ISysLoginService loginService;
        private readonly ISysPermissionService permissionService;
        private readonly ISysMenuService menuService;
        public SysLoginController(ILogger<SysLoginController> plogger, ISysTokenService pTokenService, ISysLoginService pLoginService,
            ISysMenuService pMenuService, ISysPermissionService pPermissionService)
        {
            logger = plogger;
            tokenService = pTokenService;
            loginService = pLoginService;
            menuService = pMenuService;
            permissionService = pPermissionService;
        }
        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="model">登陆信息</param>
        [AllowAnonymous]
        [HttpPost("login")]         
        public AjaxReponseBase Login([FromBody] LoginModel model)
        {
            
            AjaxReponseBase response =  AjaxReponseBase.Success();
            // 生成令牌
            string token = loginService.login(model.username, model.password, model.code,
                    model.uuid);                       
            response.Add(SystemConst.TOKEN, token);
            return response;
           // return Ok(response);
            
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("getInfo")]
        public AjaxReponseBase GetInfo()
        {
            LoginUser loginUser = tokenService.getLoginUser(this.Request);
            SysUser user = loginUser.user;
            // 角色集合
            List<string> roles = permissionService.getRolePermission(user);
            // 权限集合
            List<string> permissions = permissionService.getMenuPermission(user);
            AjaxReponseBase response = AjaxReponseBase.Success();
            response.Add("user", user);
            response.Add("roles", roles);
            response.Add("permissions", permissions);
            return response;
        }

        /// <summary>
        /// 获取路由信息
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("getRouters")]
        public AjaxReponseBase getRouters()
        {
            LoginUser loginUser = tokenService.getLoginUser(this.Request);
            // 用户信息
            SysUser user = loginUser.user;
            List<SysMenu> menus = menuService.selectMenuTreeByUserId(user.Id);
            return AjaxReponseBase.Success(menuService.buildMenus(menus));
        }
    }
}
