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
        
        private ISysDeptRepository sysDeptRepository;
       
        public SysDeptService(ISysDeptRepository psysDeptRepository)
        {

            sysDeptRepository = psysDeptRepository;
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public List<SysDept> selectDeptList(SysDept dept)
        {
            return sysDeptRepository.selectDeptList(dept);         
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="depts"></param>
        /// <returns></returns>
        public List<SysDept> buildDeptTree(List<SysDept> depts)
        {
            return sysDeptRepository.buildDeptTree(depts);    
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="depts"></param>
        /// <returns></returns>
        public List<TreeSelect> buildDeptTreeSelect(List<SysDept> depts)
        {
            return sysDeptRepository.buildDeptTreeSelect(depts);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<int> selectDeptListByRoleId(long roleId)
        {
            return sysDeptRepository.selectDeptListByRoleId(roleId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public SysDept selectDeptById(long deptId)
        {
            return sysDeptRepository.selectDeptById(deptId); 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public int selectNormalChildrenDeptById(long deptId)
        {
            return sysDeptRepository.selectNormalChildrenDeptById(deptId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public bool hasChildByDeptId(long deptId)
        {
            return sysDeptRepository.hasChildByDeptId(deptId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public bool checkDeptExistUser(long deptId)
        {
            return sysDeptRepository.checkDeptExistUser(deptId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public string checkDeptNameUnique(SysDept dept)
        {
            return sysDeptRepository.checkDeptNameUnique(dept);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public int insertDept(SysDept dept)
        {
            return sysDeptRepository.insertDept(dept);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public int updateDept(SysDept dept)
        {
            return sysDeptRepository.updateDept(dept);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public int deleteDeptById(long deptId)
        {
            return sysDeptRepository.deleteDeptById(deptId);
        }
    }
}
