using System;
using System.Text;
using Newtonsoft.Json;
using OnlineStore.Core.Contracts.Entities;
using RabbitMQ.Client;
using System.IO;
using ATCommon.Utilities;

namespace OnlineStore.MQ.RabbitMQ
{
    public class RabbitMQEntityPost<TEntity> where TEntity:class,IEntity,new()
    {
        private readonly string _queueName;
        public RabbitMQEntityPost(string queueName)
        {
           _queueName = queueName;
        }
        public string Post(TEntity data)
        {
            var returnValue = "";
            try
            {
                var rabbitMQService = new RabbitMQService();

                var queueName = AppSettingsHelper.GetAppSettings(_queueName);

                using var connection = rabbitMQService.CreateConnection();
                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
            
                var queueDate = JsonConvert.SerializeObject(data);
                var body = Encoding.UTF8.GetBytes(queueDate);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
            
                channel.BasicPublish(exchange: "",
                    routingKey: queueName,
                    basicProperties: properties,
                    body: body);

                returnValue = $"Registration was sent to the {queueName} queue.";
                Console.WriteLine(returnValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return $"[x] Sent {returnValue}";

        }
    }
}
