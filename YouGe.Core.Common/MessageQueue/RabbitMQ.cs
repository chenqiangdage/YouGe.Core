using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using YouGe.Core.Commons.Helper;
using RabbitMQ.Client;

namespace YouGe.Core.Commons.MessageQueue
{
    public class RabbitMQ:IRabbitMQ
    {
		private ConnectionFactory Factory;
		private readonly string QueueName;
		private readonly string ExchangeName;
		private readonly string RouteKey;
		public RabbitMQ(RbMQConnectConf conf)
		{
			//创建连接工厂
			Factory = new ConnectionFactory
			{
				UserName = conf.UserName,//用户名
				Password = conf.Password,//密码
				HostName = conf.HostName				 
			};
			QueueName = conf.QueueName;
			ExchangeName = conf.ExchangeName;
			RouteKey = conf.RouteKey;

		}
		/// <summary>
		/// 推送一个消息 
		/// 使用默认的系统配置的队列名
		/// </summary>
		/// <param name="msg"></param>
		public void Publish(object msg)
		{
			var connection = Factory.CreateConnection();
			//创建通道
			var channel = connection.CreateModel();
			PublishMessage(channel, QueueName, ExchangeName, RouteKey, msg);
			channel.Close();
			connection.Close();
		}
		/// <summary>
		/// 推送一个消息
		/// 推送到指定的队列名上去
		/// </summary>
		/// <param name="channel"></param>
		/// <param name="queueName"></param>
		/// <param name="exchangeName"></param>
		/// <param name="routeKey"></param>
		/// <param name="msg"></param>
		private void PublishMessage(IModel channel,string queueName, string exchangeName, string routeKey, object msg)
		{
			//定义一个Fanout类型交换机
			channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, false, false, null);
			//定义队列
			channel.QueueDeclare(queueName, false, false, false, null);
			//将队列绑定到交换机
			channel.QueueBind(queueName, exchangeName, routeKey, null);
			string message = string.Empty;
			if (msg.GetType() == typeof(string))
			{
				message = (string)msg;
			}
			else
			{
				message = JsonConvert.SerializeObject(msg);
			}

			var body = Encoding.UTF8.GetBytes(message);
			//发布消息
			try
			{
				channel.BasicPublish(exchangeName, routeKey, null, body);
			}
			catch (Exception e)
			{
				Log4NetHelper.Info(e.Message);				 
			}
		}
		/// <summary>
	    /// <summary>
		/// 推送一个消息
		/// 推送到指定的队列名上去
		/// </summary>
		/// </summary>
		/// <param name="queueName"></param>
		/// <param name="exchangeName"></param>
		/// <param name="routeKey"></param>
		/// <param name="msg"></param>
		public void Publish(string queueName, string exchangeName, string routeKey, object msg)
		{
			var connection = Factory.CreateConnection();
			//创建通道
			var channel = connection.CreateModel();
			PublishMessage(channel, queueName, exchangeName, routeKey, msg);
			channel.Close();
			connection.Close();
		}

		public void Subscribe(string RoutingKey)
		{
			throw new NotImplementedException();
		}
	}
}
