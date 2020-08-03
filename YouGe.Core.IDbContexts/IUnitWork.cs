using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Interface.IDbContexts
{
    public interface IUnitWork
    {
        bool TransationSave(int resultcount);
        int Save();

        public IDbContextTransaction StartTransation();

    }
}
