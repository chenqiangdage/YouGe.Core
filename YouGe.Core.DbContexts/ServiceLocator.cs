using System;

namespace YouGe.Core.DbContexts
{
    public static class ServiceLocator
    {
        internal static IServiceProvider Current { get; set; }

        public static T Resolve<T>()
        {
            return (T)Current.GetService(typeof(T));
        }
    }
}
