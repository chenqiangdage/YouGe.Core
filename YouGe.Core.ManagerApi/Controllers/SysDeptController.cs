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
        /**
    * 获取部门列表
    */
        //  @PreAuthorize("@ss.hasPermi('system:dept:list')")
        [HttpGet("list")]
    public AjaxReponseBase list(SysDept dept)
        {
            List<SysDept> depts = deptService.selectDeptList(dept);
            return AjaxReponseBase.Success(depts);
        }

        /**
         * 查询部门列表（排除节点）
         */
        @PreAuthorize("@ss.hasPermi('system:dept:list')")
    @GetMapping("/list/exclude/{deptId}")
    public AjaxReponseBase excludeChild(@PathVariable(value = "deptId", required = false) Long deptId)
    {
        List<SysDept> depts = deptService.selectDeptList(new SysDept());
        Iterator<SysDept> it = depts.iterator();
        while (it.hasNext())
        {
            SysDept d = (SysDept)it.next();
            if (d.getDeptId().intValue() == deptId
                    || ArrayUtils.contains(StringUtils.split(d.getAncestors(), ","), deptId + ""))
            {
                it.remove();
            }
}
        return AjaxReponseBase.Success(depts);
    }

    /**
     * 根据部门编号获取详细信息
     */
    @PreAuthorize("@ss.hasPermi('system:dept:query')")
    @GetMapping(value = "/{deptId}")
    public AjaxReponseBase getInfo(@PathVariable Long deptId)
{
    return AjaxReponseBase.Success(deptService.selectDeptById(deptId));
}

/**
 * 获取部门下拉树列表
 */
@GetMapping("/treeselect")
    public AjaxReponseBase treeselect(SysDept dept)
{
    List<SysDept> depts = deptService.selectDeptList(dept);
    return AjaxReponseBase.Success(deptService.buildDeptTreeSelect(depts));
}

/**
 * 加载对应角色部门列表树
 */
@GetMapping(value = "/roleDeptTreeselect/{roleId}")
    public AjaxReponseBase roleDeptTreeselect(@PathVariable("roleId") Long roleId)
    {
        List<SysDept> depts = deptService.selectDeptList(new SysDept());
AjaxReponseBase ajax = AjaxReponseBase.Success();
ajax.Add("checkedKeys", deptService.selectDeptListByRoleId(roleId));
        ajax.put("depts", deptService.buildDeptTreeSelect(depts));
        return ajax;
    }

    /**
     * 新增部门
     */
    @PreAuthorize("@ss.hasPermi('system:dept:add')")
    @Log(title = "部门管理", businessType = BusinessType.INSERT)
    @PostMapping
    public AjaxReponseBase add(@Validated @RequestBody SysDept dept)
{
    if (UserConstants.NOT_UNIQUE.equals(deptService.checkDeptNameUnique(dept)))
    {
        return AjaxReponseBase.Error("新增部门'" + dept.getDeptName() + "'失败，部门名称已存在");
    }
    dept.setCreateBy(SecurityUtils.getUsername());
    return toAjax(deptService.insertDept(dept));
}

/**
 * 修改部门
 */
@PreAuthorize("@ss.hasPermi('system:dept:edit')")
    @Log(title = "部门管理", businessType = BusinessType.UPDATE)
    @PutMapping
    public AjaxReponseBase edit(@Validated @RequestBody SysDept dept)
{
    if (UserConstants.NOT_UNIQUE.equals(deptService.checkDeptNameUnique(dept)))
    {
        return AjaxReponseBase.Error("修改部门'" + dept.getDeptName() + "'失败，部门名称已存在");
    }
    else if (dept.getParentId().equals(dept.getDeptId()))
    {
        return AjaxResult.error("修改部门'" + dept.getDeptName() + "'失败，上级部门不能是自己");
    }
    else if (StringUtils.equals(UserConstants.DEPT_DISABLE, dept.getStatus())
            && deptService.selectNormalChildrenDeptById(dept.getDeptId()) > 0)
    {
        return AjaxReponseBase.Error("该部门包含未停用的子部门！");
    }
    dept.setUpdateBy(SecurityUtils.getUsername());
    return toAjax(deptService.updateDept(dept));
}

/**
 * 删除部门
 */
@PreAuthorize("@ss.hasPermi('system:dept:remove')")
    @Log(title = "部门管理", businessType = BusinessType.DELETE)
    @DeleteMapping("/{deptId}")
    public AjaxReponseBase remove(@PathVariable Long deptId)
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
