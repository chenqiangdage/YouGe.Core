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
    public class SysDeptService : ISysDeptService
    {
        private ISysTokenService tokenService;
        private ISysPermissionService permissionservice;
        private ISysLoginRepository sysLoginRepository;
        private ISysConfigRepository sysConfigRepository;
        public ISysUserRepository sysUserRepository;
        public SysDeptService(ISysConfigRepository pSysConfigRepository, ISysTokenService pTokenService, ISysPermissionService pPermissionservice, ISysLoginRepository pSysLoginRepository, ISysUserRepository _sysUserRepository)
        {
            tokenService = pTokenService;
            permissionservice = pPermissionservice;
            sysLoginRepository = pSysLoginRepository;
            sysUserRepository = _sysUserRepository;
            sysConfigRepository = pSysConfigRepository;
        }

        public List<SysDept> selectDeptList(SysDept dept)
        {
            throw new NotImplementedException();
        }

        public List<SysDept> buildDeptTree(List<SysDept> depts)
        {
            throw new NotImplementedException();
        }

        public List<Models.DTModel.Sys.TreeSelect> buildDeptTreeSelect(List<SysDept> depts)
        {
            throw new NotImplementedException();
        }

        public List<int> selectDeptListByRoleId(long roleId)
        {
            throw new NotImplementedException();
        }

        public SysDept selectDeptById(long deptId)
        {
            throw new NotImplementedException();
        }

        public int selectNormalChildrenDeptById(long deptId)
        {
            throw new NotImplementedException();
        }

        public bool hasChildByDeptId(long deptId)
        {
            throw new NotImplementedException();
        }

        public bool checkDeptExistUser(long deptId)
        {
            throw new NotImplementedException();
        }

        public string checkDeptNameUnique(SysDept dept)
        {
            throw new NotImplementedException();
        }

        public int insertDept(SysDept dept)
        {
            throw new NotImplementedException();
        }

        public int updateDept(SysDept dept)
        {
            throw new NotImplementedException();
        }

        public int deleteDeptById(long deptId)
        {
            throw new NotImplementedException();
        }
    }
}
