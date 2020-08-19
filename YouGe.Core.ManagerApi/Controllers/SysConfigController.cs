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
        public SysConfigController(ISysConfigService pconfigService)
        {
            configService = pconfigService;
        }

         
     @PreAuthorize("@ss.hasPermi('system:config:list')")

        /// <summary>
        /// 获取参数配置列表
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public TableDataInfo list(SysConfig config)
        {
            startPage();
            List<SysConfig> list = configService.selectConfigList(config);
            return getDataTable(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
    @Log(title = "参数管理", businessType = BusinessType.EXPORT)
    @PreAuthorize("@ss.hasPermi('system:config:export')")
        [HttpGet("export")]      
        public AjaxReponseBase export(SysConfig config)
        {
            List<SysConfig> list = configService.selectConfigList(config);
            ExcelUtil<SysConfig> util = new ExcelUtil<SysConfig>(SysConfig.class);
            return util.exportExcel(list, "参数数据");
        }

    
    @PreAuthorize("@ss.hasPermi('system:config:query')")
        /// <summary>
        /// 根据参数编号获取详细信息
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        [HttpGet("getInfo")
        public AjaxReponseBase getInfo( long configId)
        {
            return AjaxReponseBase.Success(configService.selectConfigById(configId));
        }

    
    
        /// <summary>
        /// 根据参数键名查询参数值
        /// </summary>
        /// <param name="String"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("configKey")]
        public AjaxReponseBase getConfigKey(string configKey)
        {
            return AjaxReponseBase.Success(configService.selectConfigByKey(configKey));
        }

   
    @PreAuthorize("@ss.hasPermi('system:config:add')")
    @Log(title = "参数管理", businessType = BusinessType.INSERT)
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
            config.setCreateBy(SecurityUtils.getUsername());
            return toAjax(configService.insertConfig(config));
        }

   
    @PreAuthorize("@ss.hasPermi('system:config:edit')")
    @Log(title = "参数管理", businessType = BusinessType.UPDATE)
        /// <summary>
        /// 修改参数配置
        /// </summary>
        /// <param name="RequestBody"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        [HttpPut]
        public AjaxReponseBase edit(SysConfig config)
        {
            if (YouGeUserConstants.NOT_UNIQUE.Equals(configService.checkConfigKeyUnique(config)))
            {
                return AjaxReponseBase.Error("修改参数'" + config.ConfigName + "'失败，参数键名已存在");
            }
            config.setUpdateBy(SecurityUtils.getUsername());
            return toAjax(configService.updateConfig(config));
        }

    
    @PreAuthorize("@ss.hasPermi('system:config:remove')")
    @Log(title = "参数管理", businessType = BusinessType.DELETE)
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

    
    @PreAuthorize("@ss.hasPermi('system:config:remove')")
    @Log(title = "参数管理", businessType = BusinessType.CLEAN)
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
