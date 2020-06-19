using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YouGe.Core.Interface.IServices.IBkgTasksService
{
   public  interface IBackgroundService: IDisposable, IHostedService
    {
       abstract Task ExecuteAsync(CancellationToken stoppingToken);                  
    }
}
