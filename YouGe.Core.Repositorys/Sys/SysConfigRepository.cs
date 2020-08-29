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

namespace YouGe.Core.Repositorys.Sys
{
    public class SysConfigRepository : BaseRepository<SysConfig, int>, ISysConfigRepository
    {
        private YouGeDbContextOption option { get; set; }
        public SysConfigRepository(IYouGeDbContext dbContext) : base(dbContext)
        {
            option = (YouGeDbContextOption)DbContext.Option;
        }
        
    }
}
