using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YouGe.Core.DBEntitys.Sys;
using YouGe.Core.Models.DTModel.Sys;
using YouGe.Core.Models.System;

namespace YouGe.Core.Interface.IRepositorys.Sys
{
    public interface ISysLoginRepository : IRepository<SysLoginInfor, int>
    {
       void  recordLogininfor(string userName,char status,string message,RequestBasicInfo info);

       

        
    }
}
