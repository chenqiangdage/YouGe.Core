using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Common.Extensions
{
   public static class DateTimeExtensions
    {
         public static long CurrentTimeMillis()
        {        
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds; ;
        }
        
    }
}
