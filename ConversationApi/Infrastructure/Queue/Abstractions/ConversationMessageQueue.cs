using ConversationApi.Infrastructure.Queue.DTO;

namespace ConversationApi.Infrastructure.Queue.Abstractions
{
    public abstract class ConversationMessageQueue
    {
        public abstract ConversationMessageQueueResult Add(ConversationMessageQueueRequest conversationQueueRequest);
    }
}