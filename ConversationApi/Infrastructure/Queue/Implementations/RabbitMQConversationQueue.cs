using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using ConversationApi.Common.Settings;
using ConversationApi.Infrastructure.Queue.Abstractions;
using ConversationApi.Infrastructure.Queue.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace ConversationApi.Infrastructure.Queue.Implementations
{
    public class RabbitMQConversationQueue : ConversationQueue
    {
        private readonly AppSettings _appSettings;

        public RabbitMQConversationQueue()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            config.GetSection("App").Bind(_appSettings);
        }
        
        public RabbitMQConversationQueue(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }
        
        public override ConversationQueueResult Add(ConversationQueueRequest conversationQueueRequest)
        {
            var factory = new ConnectionFactory()
            {
                UserName = _appSettings.RabbitMQUserName,
                Password = _appSettings.RabbitMQPassword
            }; 
            
            using (var connection = factory.CreateConnection(HostNameList))
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: _appSettings.ConversationQueueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
 
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(
                    exchange: "",
                    routingKey: _appSettings.ConversationQueueName,
                    basicProperties: properties,
                    body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(conversationQueueRequest)));
            }
            return Mapper.Map<ConversationQueueResult>(conversationQueueRequest);
        }

        private List<string> HostNameList
        {
            get
            {    
                var hostNameList = new List<string>();

                if (_appSettings.RabbitMQHostNameList.Contains(";"))
                {
                    hostNameList = _appSettings.RabbitMQHostNameList.Split(';').ToList();
                }
                else
                {
                    hostNameList.Add(_appSettings.RabbitMQHostNameList);
                }

                return hostNameList;
            }
        }
        
    }
}