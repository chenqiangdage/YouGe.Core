using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.DBEntitys.Sys;

namespace YouGe.Core.Interface.IRepositorys.Sys
{
   public  interface ISysUserRepository : IRepository<SysUser, int>
    {
        SysUser selectUserByUserName(string username,string  password);
    }
}
