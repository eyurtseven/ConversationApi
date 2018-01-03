using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RestSharp;

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
            //TODO : Konfigürasyondan oku
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
                consumer.Received += OnConsumerOnReceived;
                channel.BasicConsume("ConversationQueueName-DEV", true, consumer);
                Console.ReadLine();
            }
        }

        private static void OnConsumerOnReceived(object sender, BasicDeliverEventArgs args)
        {
            var body = args.Body;
            var message = Encoding.UTF8.GetString(body);

            Task.Run(() => SendToStorage(message));
        }
        
        private static void SendToStorage(string message)
        {
            //TODO : Konfigürasyondan oku
            var client = new RestClient("http://localhost:4000/");
            var request = new RestRequest("api/v1/conversations", Method.POST);
            request.AddJsonBody(message);
            client.Execute(request);
        }
    }
}