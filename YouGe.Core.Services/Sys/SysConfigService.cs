using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using YouGe.Core.Common.Helper;
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
    public class SysConfigService : ISysConfigService
    {
        private ISysTokenService tokenService;
        private ISysPermissionService permissionservice;
        private ISysLoginRepository sysLoginRepository;
        private ISysConfigRepository sysConfigRepository;
        public ISysUserRepository sysUserRepository;
        public SysConfigService(ISysConfigRepository pSysConfigRepository,ISysTokenService pTokenService, ISysPermissionService pPermissionservice, ISysLoginRepository pSysLoginRepository, ISysUserRepository _sysUserRepository)
        {
            tokenService = pTokenService;
            permissionservice = pPermissionservice;
            sysLoginRepository = pSysLoginRepository;
            sysUserRepository = _sysUserRepository;
            sysConfigRepository = pSysConfigRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        public SysConfig selectConfigById(long configId)
        {
           return  sysConfigRepository.selectConfigById(configId);            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configKey"></param>
        /// <returns></returns>
        public SysConfig selectConfigByKey(string configKey)
        {
            return sysConfigRepository.selectConfigByKey(configKey);            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public List<SysConfig> selectConfigList(SysConfig config)
        {
           return  sysConfigRepository.selectConfigList(config);            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public int insertConfig(SysConfig config)
        {
            return sysConfigRepository.insertConfig(config);            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public int updateConfig(SysConfig config)
        {
            return sysConfigRepository.updateConfig(config);            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configIds"></param>
        /// <returns></returns>
        public int deleteConfigByIds(long[] configIds)
        {
            return sysConfigRepository.deleteConfigByIds(configIds);            
        }
        /// <summary>
        /// 
        /// </summary>
        public void clearCache()
        {
            sysConfigRepository.clearCache();            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public string checkConfigKeyUnique(SysConfig config)
        {
            return sysConfigRepository.checkConfigKeyUnique(config);           
        }
    }
}
