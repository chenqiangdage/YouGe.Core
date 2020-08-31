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
using Microsoft.AspNetCore.Http;
using YouGe.Core.Commons;
using YouGe.Core.Common.Helper;
using System.Buffers.Text;
using YouGe.Core.Commons.Helper;
using System.IO;
using YouGe.Core.Common.SystemConst;
using YouGe.Core.Common.Security;
using YouGe.Core.Common.YouGeAttribute;
using YouGe.Core.Models.Page;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using YouGe.Core.ManagerApi.Security;
using YouGe.Core.Interface.IRepositorys.Sys;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YouGe.Core.ManagerApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Route("/system/dept/[action]")]
    public class SysDeptController : YouGeController
    {
        protected readonly ISysDeptService deptService;
        protected readonly IHttpContextAccessor httpContextAccessor;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ISysTokenService tokenService;
        public SysDeptController(IHttpContextAccessor phttpContextAccessor, IHostingEnvironment _hostingEnvironment, ISysTokenService pTokenService, ISysDeptService psysDeptService)
        {
            this.httpContextAccessor = phttpContextAccessor;
            deptService = psysDeptService;
            tokenService = pTokenService;
            hostingEnvironment = _hostingEnvironment;
        }
 
        //  @PreAuthorize("@ss.hasPermi('system:dept:list')")
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public AjaxReponseBase list([FromQuery] SysDept dept)
        {
            List<SysDept> depts = deptService.selectDeptList(dept);
            return AjaxReponseBase.Success(depts);
        }


        //@PreAuthorize("@ss.hasPermi('system:dept:list')")
        /// <summary>
        /// 查询部门列表（排除节点）
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/list/exclude/{deptId}")]       
        public AjaxReponseBase excludeChild( long deptId)
        {
            List<SysDept> depts = deptService.selectDeptList(new SysDept());
            var it = depts.GetEnumerator();
            while (it.MoveNext())
            {
                SysDept d = (SysDept)it.Current;
                if (d.Id== deptId || d.Ancestors.Split(",").ToList().Contains(deptId.ToString()))
                {
                    depts.Remove(d);
                }
            }                         
        return AjaxReponseBase.Success(depts);
        }

        /// <summary>
        /// 根据部门编号获取详细信息
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        //@PreAuthorize("@ss.hasPermi('system:dept:query')")
        [Route("/system/dept/{deptId}")]
        public AjaxReponseBase getInfo(long deptId)
        {
            return AjaxReponseBase.Success(deptService.selectDeptById(deptId));
        }

        /// <summary>
        /// 获取部门下拉树列表
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        [HttpGet("/treeselect")]
        public AjaxReponseBase treeselect(SysDept dept)
        {
            List<SysDept> depts = deptService.selectDeptList(dept);
            return AjaxReponseBase.Success(deptService.buildDeptTreeSelect(depts));
        }

        /// <summary>
        /// 加载对应角色部门列表树
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("/roleDeptTreeselect")]
        public AjaxReponseBase roleDeptTreeselect(long roleId)
        {
        List<SysDept> depts = deptService.selectDeptList(new SysDept());
        AjaxReponseBase ajax = AjaxReponseBase.Success();
        ajax.Add("checkedKeys", deptService.selectDeptListByRoleId(roleId));
        ajax.Add("depts", deptService.buildDeptTreeSelect(depts));
        return ajax;
        }

        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        //@PreAuthorize("@ss.hasPermi('system:dept:add')")

        [YouGeLog(title = "部门管理", buinessType = BusinessType.INSERT)]
        [HttpPost()]
        public AjaxReponseBase add([FromBody]SysDept dept)
        {
            if (YouGeUserConstants.NOT_UNIQUE.Equals(deptService.checkDeptNameUnique(dept)))
            {
                return AjaxReponseBase.Error("新增部门'" + dept.DeptName + "'失败，部门名称已存在");
            }
            dept.CreateBy =(SecurityUtils.getUsername(tokenService, httpContextAccessor.HttpContext.Request));
            return toAjax(deptService.insertDept(dept));
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        //@PreAuthorize("@ss.hasPermi('system:dept:edit')")
        [YouGeLog(title = "部门管理", buinessType = BusinessType.UPDATE)]
        [HttpPut()]
        public AjaxReponseBase edit([FromBody] SysDept dept)
        {
            if (YouGeUserConstants.NOT_UNIQUE.Equals(deptService.checkDeptNameUnique(dept)))
            {
                return AjaxReponseBase.Error("修改部门'" + dept.DeptName + "'失败，部门名称已存在");
            }
            else if (dept.ParentId.Equals(dept.Id))
            {
                return AjaxReponseBase.Error("修改部门'" + dept.DeptName + "'失败，上级部门不能是自己");
            }
            else if ( YouGeUserConstants.DEPT_DISABLE.Equals( dept.Status)
                    && deptService.selectNormalChildrenDeptById(dept.Id) > 0)
            {
                return AjaxReponseBase.Error("该部门包含未停用的子部门！");
            }
            dept.UpdateBy=(SecurityUtils.getUsername(tokenService, httpContextAccessor.HttpContext.Request));
            return toAjax(deptService.updateDept(dept));
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        //@PreAuthorize("@ss.hasPermi('system:dept:remove')")
        [YouGeLog(title = "部门管理", buinessType = BusinessType.DELETE)]
        [HttpDelete("/system/dept/{deptId}")]         
        public AjaxReponseBase remove(long deptId)
        {
            if (deptService.hasChildByDeptId(deptId))
            {
                return AjaxReponseBase.Error("存在下级部门,不允许删除");
            }
            if (deptService.checkDeptExistUser(deptId))
            {
                return AjaxReponseBase.Error("部门存在用户,不允许删除");
            }
            return toAjax(deptService.deleteDeptById(deptId));
        }
    }
}
