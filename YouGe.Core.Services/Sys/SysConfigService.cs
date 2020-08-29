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

        public SysConfig selectConfigById(long configId)
        {
            
            throw new NotImplementedException();
        }

        public string selectConfigByKey(string configKey)
        {
            throw new NotImplementedException();
        }

        public List<SysConfig> selectConfigList(SysConfig config)
        {
            throw new NotImplementedException();
        }

        public int insertConfig(SysConfig config)
        {
            throw new NotImplementedException();
        }

        public int updateConfig(SysConfig config)
        {
            throw new NotImplementedException();
        }

        public int deleteConfigByIds(long[] configIds)
        {
            throw new NotImplementedException();
        }

        public void clearCache()
        {
            throw new NotImplementedException();
        }

        public string checkConfigKeyUnique(SysConfig config)
        {
            throw new NotImplementedException();
        }
    }
}
