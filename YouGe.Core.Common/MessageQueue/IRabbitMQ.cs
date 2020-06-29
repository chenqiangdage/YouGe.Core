using System;
using System.Collections.Generic;
using System.Text;

namespace YouGe.Core.Commons.MessageQueue
{
    public interface IRabbitMQ
    {
        /// <summary>
        /// 发布一个消息
        /// </summary>       
        /// <param name="Model"></param>
        void Publish( object Model);
        /// <summary>
        /// 推送到一个指定的消息队列上
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="exchangeName"></param>
        /// <param name="routeKey"></param>
        /// <param name="msg"></param>
        void Publish(string queueName, string exchangeName, string routeKey, object msg);
        /// <summary>
        /// 订阅一个消息
        /// </summary>      
        /// <param name="RoutingKey"></param>
        void Subscribe( string RoutingKey);
    }
}
