using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Commons.MessageQueue
{
    /// <summary>
    /// 定义一个用来注入的配置
    /// </summary>
   public  class RbMQConnectConf
    {
        public RbMQConnectConf(string userName,string password,string hostName,string queueName,string exchangeName,string routeKey)
        {
            UserName = userName;
            Password = password;
            HostName = hostName;
            QueueName = queueName;
            ExchangeName = exchangeName;
            RouteKey = routeKey;
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
        /// <summary>
        /// 队列名字
        /// </summary>
        public string QueueName { get; set; }
        /// <summary>
        /// 交换机的名字
        /// </summary>
        public string ExchangeName { get; set; }
        /// <summary>
        /// 投递消息到交换机要用的key
        /// </summary>
        public string RouteKey { get; set; }
    }
}
