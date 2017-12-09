namespace ConversationApi.Common.Settings
{
    public class AppSettings
    {
        public string RabbitMQUserName { get; set; }
        public string RabbitMQPassword { get; set; }
        public string RabbitMQHostNameList { get; set; }
        public string ConversationQueueName { get; set; }
    }
}