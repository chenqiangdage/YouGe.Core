using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using YouGe.Core.Services.BkgTasksService;

namespace YouGe.Core.ProcessService.BackGroundTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new HostBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<MyTimerHostedService>(); //托管一个定时任务 
                services.AddHostedService<MyBackGroundService>(); //定义个后台服务
                services.AddHostedService<MyBackGround2Service>(); //定义第二个后台服务
            }).Build();

           
            host.Run();
            
        }
    }
}
