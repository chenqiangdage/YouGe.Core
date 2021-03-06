﻿using System;
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

namespace YouGe.Core.ManagerApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SysConfigController : YouGeController
    {
        private ISysConfigService configService;
        protected readonly IHttpContextAccessor httpContextAccessor;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ISysTokenService tokenService;
        public SysConfigController(IHttpContextAccessor phttpContextAccessor, IHostingEnvironment _hostingEnvironment, ISysTokenService pTokenService, ISysConfigService pconfigService)
        {
            this.httpContextAccessor = phttpContextAccessor;
            configService = pconfigService;
            tokenService = pTokenService;
            hostingEnvironment = _hostingEnvironment;
        }

        // to do  
        //@PreAuthorize("@ss.hasPermi('system:config:list')")

        /// <summary>
        /// 获取参数配置列表
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public TableDataInfo<SysConfig> list([FromQuery] SysConfig config)
        {
           
            startPage(httpContextAccessor);
            long total = 1;
            List<SysConfig> list = configService.selectConfigList(config);
            return getDataTable(list,total);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>

        // to do  
        //@PreAuthorize("@ss.hasPermi('system:config:export')")

        [YouGeLog(title = "参数管理", buinessType = BusinessType.EXPORT)]
        [HttpGet("export")]
        public AjaxReponseBase export(SysConfig config)
        {
            List<SysConfig> list = configService.selectConfigList(config);
                         
            string rootpath = hostingEnvironment.WebRootPath + @"\ExcelFiles";
            if (!Directory.Exists(rootpath))
            {
                Directory.CreateDirectory(rootpath);
            }
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + config.ConfigKey + ".xlsx";
            bool exportOK =  ExcelUtil.ExportExcelToFile<SysConfig>(list, "参数数据", null, rootpath, fileName);
            if (exportOK)
            {
                return AjaxReponseBase.Success(fileName);
            }
            else
            {
                return AjaxReponseBase.Error("导出失败！");
            }
             
        }

        // to do  
        // @PreAuthorize("@ss.hasPermi('system:config:query')")
        /// <summary>
        /// 根据参数编号获取详细信息
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        [HttpGet("getInfo")]
        public AjaxReponseBase getInfo(long configId)
        {                     
            return AjaxReponseBase.Success(configService.selectConfigById(configId));
        }



        /// <summary>
        /// 根据参数键名查询参数值
        /// </summary>
        /// <param name="configKey"></param>
        /// <returns></returns>
        [HttpGet("configKey")]
        public AjaxReponseBase getConfigKey(string configKey)
        {
            return AjaxReponseBase.Success(configService.selectConfigByKey(configKey));
        }

        // to do  
        //@PreAuthorize("@ss.hasPermi('system:config:add')")

        [YouGeLog(title= "参数管理", buinessType=BusinessType.INSERT)]
        /// <summary>
        /// 新增参数配置
        /// </summary>
        /// <param name="RequestBody"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        [HttpPost]
        public AjaxReponseBase add(SysConfig config)
        {
            if (YouGeUserConstants.NOT_UNIQUE.Equals(configService.checkConfigKeyUnique(config)))
            {
                return AjaxReponseBase.Error("新增参数'" + config.ConfigName + "'失败，参数键名已存在");
            }
            config.CreateBy = (SecurityUtils.getUsername(tokenService, httpContextAccessor.HttpContext.Request));
            return toAjax(configService.insertConfig(config));
        }

        // to do  
        //@PreAuthorize("@ss.hasPermi('system:config:edit')")         
        /// <summary>
        /// 修改参数配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [HttpPut]
        [YouGeLog(title = "参数管理", buinessType = BusinessType.UPDATE)]
        public AjaxReponseBase edit(SysConfig config)
        {
            if (YouGeUserConstants.NOT_UNIQUE.Equals(configService.checkConfigKeyUnique(config)))
            {
                return AjaxReponseBase.Error("修改参数'" + config.ConfigName + "'失败，参数键名已存在");
            }         
            config.UpdateBy =(SecurityUtils.getUsername(tokenService, httpContextAccessor.HttpContext.Request));
            return toAjax(configService.updateConfig(config));
        }

        // to do  
        //@PreAuthorize("@ss.hasPermi('system:config:remove')")
        [YouGeLog(title= "参数管理", buinessType=BusinessType.DELETE)]
    /// <summary>
    /// 删除参数配置
    /// </summary>
    /// <param name="configIds"></param>
    /// <returns></returns>
    [HttpDelete("remove")]
        public AjaxReponseBase remove(long[] configIds)
        {
            return toAjax(configService.deleteConfigByIds(configIds));
        }

        // to do  
        //@PreAuthorize("@ss.hasPermi('system:config:remove')")
        [YouGeLog(title= "参数管理", buinessType= BusinessType.CLEAN)]
    /// <summary>
    /// 清空缓存
    /// </summary>
    /// <returns></returns>
    [HttpDelete("clearCache")]  
        public AjaxReponseBase clearCache()
        {
            configService.clearCache();
            return AjaxReponseBase.Success();
        }
    }
}
