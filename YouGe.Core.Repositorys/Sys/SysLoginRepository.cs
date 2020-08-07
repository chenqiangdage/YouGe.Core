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

namespace YouGe.Core.Repositorys.Sys
{
    public class SysLoginRepository : BaseRepository<SysLoginInfor, int>, ISysLoginRepository
    {

      
        public SysLoginRepository(IYouGeDbContext dbContext) : base(dbContext)
        {
          
        }
       
        public void recordLogininfor(string userName, char status, string message)
        {
            SysLoginInfor model = new SysLoginInfor();
            model.UserName = userName;
            model.status = status;
            model.msg = message;
            this.Add(model);             
        }
    }
}
