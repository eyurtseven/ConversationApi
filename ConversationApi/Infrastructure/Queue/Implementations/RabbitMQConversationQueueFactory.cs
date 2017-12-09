using ConversationApi.Infrastructure.Queue.Abstractions;

namespace ConversationApi.Infrastructure.Queue.Implementations
{
    public class RabbitMQConversationQueueFactory : ConversationQueueFactory
    {
        public override ConversationQueue Create()
        {
            return new RabbitMQConversationQueue();
        }
    }
}