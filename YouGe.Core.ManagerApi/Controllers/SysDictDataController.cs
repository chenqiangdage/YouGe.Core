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
    public class SysDictDataController : YouGeController
    {


        protected readonly IHttpContextAccessor httpContextAccessor;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ISysTokenService tokenService;

        private ISysDictDataService dictDataService;
        private ISysDictTypeService dictTypeService;
        public SysDictDataController(IHttpContextAccessor phttpContextAccessor, IHostingEnvironment _hostingEnvironment, ISysTokenService pTokenService,
            ISysDictDataService pdictDataService, ISysDictTypeService pdictTypeService)
        {
            this.httpContextAccessor = phttpContextAccessor;
            dictDataService = pdictDataService;
            tokenService = pTokenService;
            dictTypeService = pdictTypeService;
            hostingEnvironment = _hostingEnvironment;
        }

        //  @PreAuthorize("@ss.hasPermi('system:dict:list')")
        [HttpGet("list")]
        public TableDataInfo<SysDictData> list(SysDictData dictData)
        {
            startPage(httpContextAccessor);
            long total = 1;
            List<SysDictData> list = dictDataService.selectDictDataList(dictData);
            return getDataTable(list, total);
        }

        [YouGeLog(title = "字典数据", buinessType = BusinessType.EXPORT)]

        // @PreAuthorize("@ss.hasPermi('system:dict:export')")
        [HttpGet("export")]
        public AjaxReponseBase export(SysDictData dictData)
        {
            string rootpath = hostingEnvironment.WebRootPath + @"\ExcelFiles";
            if (!Directory.Exists(rootpath))
            {
                Directory.CreateDirectory(rootpath);
            }
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + dictData.DictType + ".xlsx";
            List<SysDictData> list = dictDataService.selectDictDataList(dictData);
            bool exportOK = ExcelUtil.ExportExcelToFile<SysDictData>(list, "参数数据", null, rootpath, fileName);
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
     * 查询字典数据详细
     */
    // @PreAuthorize("@ss.hasPermi('system:dict:query')")
    [HttpGet("getInfo")]
    public AjaxReponseBase getInfo(long dictCode)
    {
        return AjaxReponseBase.Success(dictDataService.selectDictDataById(dictCode));
    }

    /**
     * 根据字典类型查询字典数据信息
     */
    [HttpGet("type")]
     
    public AjaxReponseBase dictType( string dictType)
    {
        return AjaxReponseBase.Success(dictTypeService.selectDictDataByType(dictType));
    }

    /**
     * 新增字典类型
     */
    //  @PreAuthorize("@ss.hasPermi('system:dict:add')")
    [YouGeLog(title = "字典数据", buinessType = BusinessType.INSERT)]
   
    [HttpPost("add")]
    public AjaxReponseBase Add([FromBody] SysDictData dict)
    {
        dict.AjaxReponseBase(SecurityUtils.getUsername());
        return toAjax(dictDataService.insertDictData(dict));
    }

    /**
     * 修改保存字典类型
     */
    // @PreAuthorize("@ss.hasPermi('system:dict:edit')")
    [YouGeLog(title = "字典数据", buinessType = BusinessType.UPDATE)]
    [HttpPut]
    public AjaxReponseBase edit([FromBody] SysDictData dict)
    {
        dict.setUpdateBy(SecurityUtils.getUsername());
        return toAjax(dictDataService.updateDictData(dict));
    }

    /**
     * 删除字典类型
     */
    //  @PreAuthorize("@ss.hasPermi('system:dict:remove')")
    [YouGeLog(title = "字典数据", buinessType = BusinessType.DELETE)]
   [HttpDelete]
    public AjaxReponseBase remove( long[] dictCodes)
    {
        return toAjax(dictDataService.deleteDictDataByIds(dictCodes));
    }
}
}

