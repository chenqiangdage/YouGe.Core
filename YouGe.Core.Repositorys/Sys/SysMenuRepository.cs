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
   public  class SysMenuRepository : BaseRepository<SysMenu, int>, ISysMenuRepository
    {
        private YouGeDbContextOption option { get; set; }

        public IYouGeDbContext _dataContext;
        public SysMenuRepository(IYouGeDbContext dbContext) : base(dbContext)
        {
            _dataContext = dbContext;
            option = (YouGeDbContextOption)DbContext.Option;
        }

        public List<string> selectMenuPermsByUserId(long userId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" select distinct m.perms
		     from sys_menu m
			 left join sys_role_menu rm on m.menu_id = rm.menu_id
			 left join sys_user_role ur on rm.role_id = ur.role_id
			 left join sys_role r on r.role_id = ur.role_id
		    where m.status = '0' and r.status = '0' and ur.user_id = @userId");
            
            MySqlParameter[] parametera = new MySqlParameter[1]{
                new MySqlParameter("userId", MySqlDbType.Int64)
            };
            parametera[0].Value = userId;

            var models = _dataContext.GetDatabase().SqlQuery<SysMenuPerms>(sql.ToString(), parametera);

            List<string> permsSet = new List<string>();
            foreach (SysMenuPerms perm in models)
            {
                if (null != perm)
                {
                    permsSet.AddRange(perm.perms.Trim().Split(",").ToList());
                }
            }
            return permsSet;
        }
    }
}
