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
using MySql.Data.MySqlClient;
using System.Linq;

namespace YouGe.Core.Repositorys.Sys
{
   public  class SysDeptRepository : BaseRepository<SysDept, int>, ISysDeptRepository
    {
        private YouGeDbContextOption option { get; set; }
        public IYouGeDbContext _dataContext;
        public SysDeptRepository(IYouGeDbContext dbContext) : base(dbContext)
        {
            _dataContext = dbContext;
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
            if (0!=dept.ParentId)
            {
                express = express.AndAlso(e => e.ParentId ==dept.ParentId);
            }
            if (!string.IsNullOrEmpty(dept.DeptName))
            {
                express = express.AndAlso(e => e.DeptName.Contains( dept.DeptName));
            }
            if (dept.Status.ToString() != "")
            {
                express = express.AndAlso(e => e.Status == dept.Status);
            }
            //to do 如何排序？
            return (List<SysDept>)this.Get(express);
        }

        

        public List<int> selectDeptListByRoleId(long roleId)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append(@"select d.dept_id, d.parent_id
		from sys_dept d
            left join sys_role_dept rd on d.dept_id = rd.dept_id
        where rd.role_id = @roleId
        	and d.dept_id not in (select d.parent_id from sys_dept d inner join sys_role_dept rd on d.dept_id = rd.dept_id and rd.role_id = @roleId)
		order by d.parent_id, d.order_num");

            MySqlParameter[] parameters = new MySqlParameter[1]{
                new MySqlParameter("userId", MySqlDbType.Int64)
            };
            parameters[0].Value = roleId;

            List<SysDept> models = _dataContext.GetDatabase().SqlQuery<SysDept>(sql.ToString(), parameters);
            return models.Select(u => u.Id).ToList();
        }

        public SysDept selectDeptById(long deptId)
        {
           return   this.GetSingleOrDefault(u => u.Id == deptId);            
        }

        public int selectNormalChildrenDeptById(long deptId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" select count(*) as Tcount  from sys_dept where status = 0 and del_flag = '0' and find_in_set(@deptId, ancestors)");
            MySqlParameter[] parametera = new MySqlParameter[1]{
                new MySqlParameter("userId", MySqlDbType.Int64)
            };
            parametera[0].Value = deptId;
            DBCount models = _dataContext.GetDatabase().SqlQuery<DBCount>(sql.ToString(), parametera).FirstOrDefault();
            return (models != null)? models.Tcount : 0;
        }

        public int hasChildByDeptId(long deptId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"  select count(1) as Tcount from sys_dept
        where del_flag = '0' and parent_id = @deptId");
            MySqlParameter[] parameters = new MySqlParameter[1]{
                new MySqlParameter("deptId", MySqlDbType.Int64)
            };
            parameters[0].Value = deptId;
            DBCount models = _dataContext.GetDatabase().SqlQuery<DBCount>(sql.ToString(), parameters).FirstOrDefault();
            return (models != null) ? models.Tcount : 0;
           
             
        }

        public int checkDeptExistUser(long deptId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"   select count(1) as Tcount from sys_user where dept_id = @deptId and del_flag = '0'");
            MySqlParameter[] parameters = new MySqlParameter[1]{
                new MySqlParameter("deptId", MySqlDbType.Int64)
            };
            parameters[0].Value = deptId;
            DBCount models = _dataContext.GetDatabase().SqlQuery<DBCount>(sql.ToString(), parameters).FirstOrDefault();
            return (models != null) ? models.Tcount : 0;
           
        }

        public SysDept checkDeptNameUnique(string deptName,long deptParentId)
        {
           return  this.GetSingleOrDefault(u => u.DeptName == deptName && u.ParentId == deptParentId);
        }

        public int insertDept(SysDept dept)
        {
           return this.Add(dept);
          
        }

        public int updateDept(SysDept dept)
        {
           return  this.Update(dept);
             
        }

        public int deleteDeptById(long deptId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"   update sys_dept set del_flag = '2' where dept_id = @deptId");
            MySqlParameter[] parameters = new MySqlParameter[1]{
                new MySqlParameter("deptId", MySqlDbType.Int64)
            };
            parameters[0].Value = deptId;
            return this.ExecuteSql(sql.ToString(),parameters);
             
        }
        /// <summary>
        /// 根据ID查询所有子部门
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns>部门列表</returns>
        public List<SysDept> selectChildrenDeptById(long deptId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" select * from sys_dept where find_in_set(@deptId, ancestors)");
            MySqlParameter[] parameters = new MySqlParameter[1]{
                new MySqlParameter("deptId", MySqlDbType.Int64)
            };
            parameters[0].Value = deptId;
             List <SysDept> models = _dataContext.GetDatabase().SqlQuery<SysDept>(sql.ToString(), parameters);
            return models;
        }

        /// <summary>
        /// 修改子元素关系
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public int updateDeptChildren(List<SysDept> depts)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"  update sys_dept set ancestors = ");
            foreach(var item in depts )
            {
                sql.Append( string.Format(@"  case dept_id  when {0} then {1} end ", item.Id, item.Ancestors));
            }
            sql.Append(@"   where dept_id in ( ");
            for (int i = 0;i<  depts.Count;i++)
            {
                if (i + 1 == depts.Count)
                {
                    sql.Append(string.Format(@" {0} ", depts[i].Id));
                }
                else
                {
                    sql.Append(string.Format(@" {0},", depts[i].Id));
                }
            }
            sql.Append(@" ) ");

           return  this.ExecuteSql(sql.ToString());
        }

        /// <summary>
        /// 修改所在部门的父级部门状态
        /// </summary>
        /// <param name="dept"></param>
        public void updateDeptStatus(SysDept dept)
        {
            this.Update(dept);
        }

    }
}
