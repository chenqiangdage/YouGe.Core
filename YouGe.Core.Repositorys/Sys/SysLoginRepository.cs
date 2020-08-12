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
    public class SysLoginRepository : BaseRepository<SysLoginInfor, int>, ISysLoginRepository
    {
       
        private YouGeDbContextOption option { get; set; }
        public SysLoginRepository(IYouGeDbContext dbContext) : base(dbContext)
        {
             
        }
       
        public  void recordLogininfor(string userName, char status, string message, RequestBasicInfo info)
        {
            SysLoginInfor model = new SysLoginInfor();
            model.UserName = userName;
            model.status = status;
            model.msg = message;
            var task = IPAddressHelper.getRealAddressByIP(info.Ip);
            model.ipaddr = info.Ip;
            model.LoginLocation = task.Result;
            model.Browser = info.Device;
            model.Os = info.Os;
            model.LoginTime = DateTime.Now;
            //  this.Add(model);

            using (var context = new YouGeDbContext(option))
            {
                context.Set<SysLoginInfor>().Add(model);
                context.SaveChanges();
            }



        }

    
    }
}
