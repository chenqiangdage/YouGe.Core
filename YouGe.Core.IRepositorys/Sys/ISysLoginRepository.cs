using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.DBEntitys.Sys;
using YouGe.Core.Models.DTModel.Sys;

namespace YouGe.Core.Interface.IRepositorys.Sys
{
    public interface ISysLoginRepository : IRepository<SysLoginInfor, int>
    {
        void recordLogininfor(string userName,string status,string message);

        LoginUser loadUserByUsername(string username,string password);

        
    }
}
