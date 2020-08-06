using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using YouGe.Core.Interface.IDbContexts;

namespace YouGe.Core.DbContexts
{
    public class YouGeDbContext : MySqlDbContext, IYouGeDbContext
    {
        public YouGeDbContext(YouGeDbContextOption option) : base(option)
        {
        }
        public YouGeDbContext(IOptions<YouGeDbContextOption> option) : base(option)
        {
        }
    }
}
