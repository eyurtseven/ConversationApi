using ConversationApi.Infrastructure.Queue.Abstractions;

namespace ConversationApi.Infrastructure.Queue.Implementations
{
    public class RabbitMQConversationMessageQueueFactory : ConversationMessageQueueFactory
    {
        public override ConversationMessageQueue Create()
        {
            return new RabbitMQConversationMessageQueue();
        }
    }
}