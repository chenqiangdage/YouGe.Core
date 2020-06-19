using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YouGe.Core.Interface.IServices.IBkgTasksService;

namespace YouGe.Core.Services.BkgTasksService
{
    public class MyTimerHostedService :IMyTimerHostedService,IDisposable
    {
        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("启动定时任务托管服务");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(0.5));

            return Task.CompletedTask;
        }

        public  void DoWork(object state)
        {
            Console.WriteLine("定时任务处理中,在DoWork中处理 定时任务要处理的业务");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            Console.WriteLine("停止定时任务");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // 手动释放定时器
            _timer?.Dispose();
        }
    }
}
