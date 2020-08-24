using System;
using System.Reflection;
using System.Threading;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using YouGe.Core.Common.YouGeAttribute;

namespace YouGe.Core.Common.AOP
{
    public class LogAop:IInterceptor
    {
        private readonly IHttpContextAccessor _accessor;

        private static readonly string FileName = "AOPInterceptor-" + DateTime.Now.ToString("yyyyMMddHH") + ".log";

        //支持单个写线程和多个读线程的锁
        private static readonly ReaderWriterLockSlim Lock = new ReaderWriterLockSlim();

        public LogAop(IHttpContextAccessor accessor)
        {
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public void Intercept(IInvocation invocation)
        {

           
            var  attrs = invocation.Method.GetCustomAttributes(typeof(YouGeLogAttribute),true);
            foreach(var item in attrs)
            {
                if (item.GetType() == typeof(YouGeLogAttribute))
                {
                    YouGeLogAttribute attr = item as YouGeLogAttribute;
                    Console.WriteLine("自定义特性 记录日志 Name：" +attr.title+ ", 元数据：" + attr.buinessType);
                }
            }
            if(attrs != null&& attrs.Length>0)
{
               
              
            }


        }
    }
}
