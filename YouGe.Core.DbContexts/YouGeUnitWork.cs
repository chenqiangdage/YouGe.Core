using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.Interface.IDbContexts;

namespace YouGe.Core.DbContexts
{
    public class YouGeUnitWork: IYouGeUnitWork
    {
        public IYouGeDbContext _baseDbContext;
        public YouGeUnitWork(IYouGeDbContext baseDbContext)
        {
            _baseDbContext = baseDbContext;
        }

        public IDbContextTransaction StartTransation()
        {
            return this._baseDbContext.GetDatabase().BeginTransaction();
        }

        public bool TransationSave(int resultcount)
        {
            using (var tran = this._baseDbContext.GetDatabase().BeginTransaction())
            {
                int result = this._baseDbContext.SaveChanges();
                if (result == resultcount)
                {
                    tran.Commit();
                    return true;
                }
                else
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        public int Save()
        {
            return this._baseDbContext.SaveChanges();
        }
    }
}
