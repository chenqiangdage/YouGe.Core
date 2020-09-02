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
        public List<TreeSelect> buildDeptTreeSelect(List<SysDept> depts)
        {
            List<SysDept> deptTrees = buildDeptTree(depts);
            List<TreeSelect> list = new List<TreeSelect>();
            for (int i =0;i< deptTrees.Count;i++)
            {
                list.Add(new TreeSelect());
            }
            return list;
           // return deptTrees.stream().map(TreeSelect::new).collect(Collectors.toList());
        }

        /**
     * 构建前端所需要树结构
     * 
     * @param depts 部门列表
     * @return 树结构列表
     */
        
    public List<SysDept> buildDeptTree(List<SysDept> depts)
        {
            List<SysDept> returnList = new List<SysDept>();
            List<long> tempList = new List<long>();
            foreach (SysDept dept in depts)
            {
                tempList.Add(dept.Id);
            }

           
             var it = depts.GetEnumerator();
            while (it.MoveNext())
            {
                SysDept dept = (SysDept)it.Current;
                // 如果是顶级节点, 遍历该父节点的所有子节点
                if (!tempList.Contains(dept.ParentId))
                {
                    recursionFn(depts, dept);
                    returnList.Add(dept);
                }
            }
            if (returnList.Count>0)
            {
                returnList = depts;
            }
            return returnList;
        }

        /**
    * 递归列表
    */
        private void recursionFn(List<SysDept> list, SysDept t)
        {
            // 得到子节点列表
            List<SysDept> childList = getChildList(list, t);
            t.setChildren(childList);
            foreach (SysDept tChild in childList)
            {
                if (hasChild(list, tChild))
                {
                    // 判断是否有子节点
                    var it = childList.GetEnumerator();
                    while (it.MoveNext())
                    {
                        SysDept n = (SysDept)it.Current;
                        recursionFn(list, n);
                    }
                }
            }
        }
        /**
    * 得到子节点列表
    */
        private List<SysDept> getChildList(List<SysDept> list, SysDept t)
        {
            List<SysDept> tlist = new List<SysDept>();
            var it = list.GetEnumerator();
            while (it.MoveNext())
            {
                SysDept n = (SysDept)it.Current;
                if (n.ParentId!=0 && n.ParentId == t.Id)
                {
                    tlist.Add(n);
                }
            }
            return tlist;
        }

        /**
         * 判断是否有子节点
         */
        private bool hasChild(List<SysDept> list, SysDept t)
        {
            return getChildList(list, t).Count > 0 ? true : false;
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
            int result =  sysDeptRepository.hasChildByDeptId(deptId);
            return result > 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public bool checkDeptExistUser(long deptId)
        {
            int result = sysDeptRepository.checkDeptExistUser(deptId);
            return result > 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public string checkDeptNameUnique(SysDept dept)
        {
            long deptId = dept.Id == 0 ? -1L : dept.Id;
            
            SysDept info = sysDeptRepository.checkDeptNameUnique(dept.DeptName, dept.ParentId);
            if(info!=null && info.Id != dept.Id)
            {
                return YouGeUserConstants.NOT_UNIQUE;
            }
            return YouGeUserConstants.UNIQUE;
          
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public int insertDept(SysDept dept)
        {
            SysDept info = sysDeptRepository.selectDeptById(dept.ParentId);
            // 如果父节点不为正常状态,则不允许新增子节点
            if (!YouGeUserConstants.DEPT_NORMAL.Equals(info.Status))
            {
                throw new CustomException("部门停用，不允许新增");
            }
            dept.Ancestors =info.Ancestors + "," + dept.ParentId;
        
            return sysDeptRepository.insertDept(dept);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public int updateDept(SysDept dept)
        {
            SysDept newParentDept = sysDeptRepository.selectDeptById(dept.ParentId);
            SysDept oldDept = sysDeptRepository.selectDeptById(dept.Id);

            if (null !=newParentDept && null !=oldDept)
            {
                string newAncestors = newParentDept.Ancestors + "," + newParentDept.Id;
                string oldAncestors = oldDept.Ancestors;
                dept.Ancestors = (newAncestors);
                updateDeptChildren(dept.Id, newAncestors, oldAncestors);
            }
            int result = sysDeptRepository.updateDept(dept);
            if (YouGeUserConstants.DEPT_NORMAL.Equals(dept.Status))
            {
                // 如果该部门是启用状态，则启用该部门的所有上级部门
                updateParentDeptStatus(dept);
            }
            return result;
             
        }

        /**
    * 修改子元素关系
    * 
    * @param deptId 被修改的部门ID
    * @param newAncestors 新的父ID集合
    * @param oldAncestors 旧的父ID集合
    */
        public void updateDeptChildren(long deptId, string newAncestors, string oldAncestors)
        {
            List<SysDept> children = sysDeptRepository.selectChildrenDeptById(deptId);
            foreach (SysDept child in children)
            {
                child.Ancestors = (child.Ancestors.Replace(oldAncestors, newAncestors));
            }
            if (children.Count > 0)
            {
                sysDeptRepository.updateDeptChildren(children);
            }
        }

        /**
     * 修改该部门的父级部门状态
     * 
     * @param dept 当前部门
     */
        private void updateParentDeptStatus(SysDept dept)
        {
            String updateBy = dept.UpdateBy;
            dept = sysDeptRepository.selectDeptById(dept.Id);
            dept.UpdateBy =(updateBy);
            sysDeptRepository.updateDeptStatus(dept);
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
