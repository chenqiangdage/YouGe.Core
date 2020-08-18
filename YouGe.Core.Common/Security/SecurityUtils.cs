using System;
namespace YouGe.Core.Common.Security
{
    public class SecurityUtils
    {
        public SecurityUtils()
        {
        }

        public static bool isAdmin(long userId)
        {
            return userId != 0 && 1L == userId;
        }
    }
}
