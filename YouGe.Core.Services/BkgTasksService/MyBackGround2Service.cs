using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YouGe.Core.Services.BkgTasksService
{
    public class MyBackGround2Service:BackGroundBaseService
    {
        public  override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("后台服务2 准备开始");
            int i = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("后台服务2 正在运行" + i.ToString());
                i++;
                if (i == 100)
                    break;
                //延迟500毫秒执行 相当于使用了定时器
                await Task.Delay(100, stoppingToken);
            }

        }
    }
}
