using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.DBEntitys.Sys;
using YouGe.Core.Models.DTModel.Sys;
using YouGe.Core.Models.System;

namespace YouGe.Core.Interface.IRepositorys.Sys
{
   public interface ISysDeptRepository : IRepository<SysDept, int>
    {

        /**
         * 查询部门管理数据
         * 
         * @param dept 部门信息
         * @return 部门信息集合
         */
        public List<SysDept> selectDeptList(SysDept dept);

        
        /**
         * 根据角色ID查询部门树信息
         * 
         * @param roleId 角色ID
         * @return 选中部门列表
         */
        public List<int> selectDeptListByRoleId(long roleId);

        /**
         * 根据部门ID查询信息
         * 
         * @param deptId 部门ID
         * @return 部门信息
         */
        public SysDept selectDeptById(long deptId);

        /// <summary>
        /// 根据ID查询所有子部门
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public List<SysDept> selectChildrenDeptById(long deptId);
        /**
         * 根据ID查询所有子部门（正常状态）
         * 
         * @param deptId 部门ID
         * @return 子部门数
         */
        public int selectNormalChildrenDeptById(long deptId);

        /**
         * 是否存在部门子节点
         * 
         * @param deptId 部门ID
         * @return 结果
         */
        public int hasChildByDeptId(long deptId);

        /**
         * 查询部门是否存在用户
         * 
         * @param deptId 部门ID
         * @return 结果 true 存在 false 不存在
         */
        public int checkDeptExistUser(long deptId);

        /**
         * 校验部门名称是否唯一
         * 
         * @param dept 部门信息
         * @return 结果
         */
        public SysDept checkDeptNameUnique(string DeptName, long ParentId);

        /**
         * 新增保存部门信息
         * 
         * @param dept 部门信息
         * @return 结果
         */
        public int insertDept(SysDept dept);

        /**
         * 修改保存部门信息
         * 
         * @param dept 部门信息
         * @return 结果
         */
        public int updateDept(SysDept dept);
        /// <summary>
        /// 修改子元素关系
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public int updateDeptChildren( List<SysDept> depts);

        /// <summary>
        /// 修改所在部门的父级部门状态
        /// </summary>
        /// <param name="dept"></param>
        public void updateDeptStatus(SysDept dept);
        /**
         * 删除部门管理信息
         * 
         * @param deptId 部门ID
         * @return 结果
         */
        public int deleteDeptById(long deptId);
    }
}
