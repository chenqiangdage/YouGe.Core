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
using MySql.Data.MySqlClient;
using System.Linq;

namespace YouGe.Core.Repositorys.Sys
{
   public  class SysPermissionRepository : BaseRepository<SysRole, int>, ISysPermissionRepository
    {
        private YouGeDbContextOption option { get; set; }

        public IYouGeDbContext _dataContext;
        public SysPermissionRepository(IYouGeDbContext dbContext) : base(dbContext)
        {
            _dataContext = dbContext;
            option = (YouGeDbContextOption)DbContext.Option;
        }

        /// <summary>
        /// 根据用户ID查询权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>权限列表</returns>
        public List<string> selectRolePermissionByUserId(long userId)
        {
         
            StringBuilder sql = new StringBuilder();
            sql.Append(@" select distinct r.role_id, r.role_name, r.role_key, r.role_sort, r.data_scope,
                          r.status, r.del_flag, r.create_time, r.remark
                          from sys_role r
                          left join sys_user_role ur on ur.role_id = r.role_id
                          left join sys_user u on u.user_id = ur.user_id
                          left join sys_dept d on u.dept_id = d.dept_id");
            sql.Append("  WHERE r.del_flag = '0' and ur.user_id = @userId ");                        
            MySqlParameter[] parametera = new MySqlParameter[1]{
                new MySqlParameter("userId", MySqlDbType.Int64)                 
            };
            parametera[0].Value = userId;                                                   

            var models = _dataContext.GetDatabase().SqlQuery<SysRole>(sql.ToString(), parametera);

            List<string> permsSet = new List<string>();
            foreach(SysRole perm in models)
            {
                if (null!= perm)
                {
                    permsSet.AddRange(perm.RoleKey.Trim().Split(",").ToList());
                }
            }
            return permsSet;

        }
    }
}
