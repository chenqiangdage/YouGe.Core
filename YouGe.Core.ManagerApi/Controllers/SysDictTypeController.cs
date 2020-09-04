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
using YouGe.Core.Services.Sys;

namespace YouGe.Core.ManagerApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Route("/system/dict/[action]")]
    public class SysDictTypeController : YouGeController
    {
        protected readonly IHttpContextAccessor httpContextAccessor;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ISysTokenService tokenService;

        private ISysDictDataService dictDataService;
        private ISysDictTypeService dictTypeService;
        public SysDictTypeController(IHttpContextAccessor phttpContextAccessor, IHostingEnvironment _hostingEnvironment, ISysTokenService pTokenService,
            ISysDictDataService pdictDataService, ISysDictTypeService pdictTypeService)
        {
            this.httpContextAccessor = phttpContextAccessor;
            dictDataService = pdictDataService;
            tokenService = pTokenService;
            dictTypeService = pdictTypeService;
            hostingEnvironment = _hostingEnvironment;
        }

        // @PreAuthorize("@ss.hasPermi('system:dict:list')")
        [HttpGet("list")]
        public TableDataInfo<SysDictType> list(SysDictType dictType)
        {
            startPage(httpContextAccessor);
            long total = 1;
            List<SysDictType> list = dictTypeService.selectDictTypeList(dictType);
            return getDataTable(list, total);
        }

        [YouGeLog(title = "字典类型", buinessType = BusinessType.EXPORT)]

        //@PreAuthorize("@ss.hasPermi('system:dict:export')")
        [HttpGet("export")]
        public AjaxReponseBase export(SysDictType dictType)
        {
            string rootpath = hostingEnvironment.WebRootPath + @"\ExcelFiles";
            if (!Directory.Exists(rootpath))
            {
                Directory.CreateDirectory(rootpath);
            }
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + dictType.DictType + ".xlsx";

            List<SysDictType> list = dictTypeService.selectDictTypeList(dictType);
            bool exportOK = ExcelUtil.ExportExcelToFile<SysDictType>(list, "参数数据", null, rootpath, fileName);
            if (exportOK)
            {
                return AjaxReponseBase.Success(fileName);
            }
            else
            {
                return AjaxReponseBase.Error("导出失败！");
            }
         
        }

        /**
         * 查询字典类型详细
         */
        // @PreAuthorize("@ss.hasPermi('system:dict:query')")
        [HttpGet("getInfo")]
        public AjaxReponseBase getInfo(long dictId)
    {
        return AjaxReponseBase.Success(dictTypeService.selectDictTypeById(dictId));
    }

        /**
         * 新增字典类型
         */
        // @PreAuthorize("@ss.hasPermi('system:dict:add')")
        [YouGeLog(title = "字典类型", buinessType = BusinessType.INSERT)]
        [HttpPost("add")]
    public AjaxReponseBase add([FromBody]SysDictType dict)
    {
        if (YouGeUserConstants.NOT_UNIQUE.Equals(dictTypeService.checkDictTypeUnique(dict)))
        {
            return AjaxReponseBase.Error("新增字典'" + dict.DictName + "'失败，字典类型已存在");
        }
            string username = SecurityUtils.getUsername(tokenService, httpContextAccessor.HttpContext.Request);
            dict.CreateBy = (username);
            
        return toAjax(dictTypeService.insertDictType(dict));
    }

        /**
         * 修改字典类型
         */
        // @PreAuthorize("@ss.hasPermi('system:dict:edit')")
        [YouGeLog(title = "字典类型", buinessType = BusinessType.UPDATE)]
        
    [HttpPut]
    public AjaxReponseBase edit([FromBody] SysDictType dict)
    {
        if (YouGeUserConstants.NOT_UNIQUE.Equals(dictTypeService.checkDictTypeUnique(dict)))
        {
            return AjaxReponseBase.Error("修改字典'" + dict.DictName + "'失败，字典类型已存在");
        }
            string username = SecurityUtils.getUsername(tokenService, httpContextAccessor.HttpContext.Request);
            dict.UpdateBy = username;
        return toAjax(dictTypeService.updateDictType(dict));
    }

        /**
         * 删除字典类型
         */
        //@PreAuthorize("@ss.hasPermi('system:dict:remove')")
        [YouGeLog(title = "字典类型", buinessType = BusinessType.DELETE)]
       [HttpDelete]
    public AjaxReponseBase remove(long[] dictIds)
    {
        return toAjax(dictTypeService.deleteDictTypeByIds(dictIds));
    }

        /**
         * 清空缓存
         */
        // @PreAuthorize("@ss.hasPermi('system:dict:remove')")
        [YouGeLog(title = "字典类型", buinessType = BusinessType.CLEAN)]
       [HttpDelete("clearCache")]
    public AjaxReponseBase clearCache()
    {
        dictTypeService.clearCache();
        return AjaxReponseBase.Success();
    }

    /**
     * 获取字典选择框列表
     */
   [HttpGet("optionselect")]
    public AjaxReponseBase optionselect()
    {
        List<SysDictType> dictTypes = dictTypeService.selectDictTypeAll();
        return AjaxReponseBase.Success(dictTypes);
    }
}
}
