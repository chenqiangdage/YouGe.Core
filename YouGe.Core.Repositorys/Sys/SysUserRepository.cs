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
    public class SysUserRepository : BaseRepository<SysUser, int>, ISysUserRepository
    {
        public SysUserRepository(IYouGeDbContext dbContext) : base(dbContext)
        {
        }
        SysUser ISysUserRepository.selectUserByUserName(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
