using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouGe.Core.IdentityServer
{
    public class Config
    {
        public IEnumerable<ApiResource> Apis => new List<ApiResource> { new ApiResource("api1", "My Api") };

    }
}
