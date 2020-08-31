using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.Interface.IRepositorys.Sys;
using YouGe.Core.DBEntitys.Sys;
using YouGe.Core.Interface.IDbContexts;
using YouGe.Core.Models.DTModel.Sys;
using YouGe.Core.Common.YouGeException;
using YouGe.Core.Commons.Helper;
using YouGe.Core.Common.SystemConst;
using YouGe.Core.Models.System;
using YouGe.Core.Common.Helper;
using System.Threading.Tasks;
using YouGe.Core.DbContexts;
using System.Linq.Expressions;
using YouGe.Core.Commons;

namespace YouGe.Core.Repositorys.Sys
{
   public  class SysDeptRepository : BaseRepository<SysDept, int>, ISysDeptRepository
    {
        private YouGeDbContextOption option { get; set; }
        public SysDeptRepository(IYouGeDbContext dbContext) : base(dbContext)
        {
            option = (YouGeDbContextOption)DbContext.Option;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public List<SysDept> selectDeptList(SysDept dept)
        {
            Expression<Func<SysDept, bool>> express = i => 1 == 1;
            if (!string.IsNullOrEmpty(dept.ParentId))
            {
                express = express.AndAlso(e => e.ParentId ==dept.ParentId);
            }
            if (!string.IsNullOrEmpty(dept.DeptName))
            {
                express = express.AndAlso(e => e.DeptName.Contains( dept.ParentId));
            }
            if (dept.Status.ToString() != "")
            {
                express = express.AndAlso(e => e.Status == dept.Status);
            }
            //to do 如何排序？
            return (List<SysDept>)this.Get(express);
        }

        public List<SysDept> buildDeptTree(List<SysDept> depts)
        {
            throw new NotImplementedException();
        }

        public List<TreeSelect> buildDeptTreeSelect(List<SysDept> depts)
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
