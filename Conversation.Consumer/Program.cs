using System;
using System.Security.Cryptography;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Conversation.Consumer
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("--Consumer Started--");

            Consume();

            Console.WriteLine("--Consumer Finished--");
        }

        private static void Consume()
        {
            var connection = new ConnectionFactory
                {
                    HostName = "207.154.219.174",
                    Port = 5672,
                    VirtualHost = "/",
                    UserName = "user",
                    Password = "user"
                }
                .CreateConnection();

            using (var channel = connection.CreateModel())
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, args) =>
                {
                    var body = args.Body;
                    var message = Encoding.UTF8.GetString(body);

                    Console.WriteLine(message);
                };
                channel.BasicConsume("ConversationQueueName-DEV", true, consumer);
                Console.ReadLine();
            }
        }
    }
}