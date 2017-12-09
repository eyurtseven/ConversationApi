using ConversationApi.Infrastructure.Queue.DTO;

namespace ConversationApi.Infrastructure.Queue.Abstractions
{
    public abstract class ConversationQueue
    {
        public abstract ConversationQueueResult Add(ConversationQueueRequest conversationQueueRequest);
    }
}