using ConversationApi.Infrastructure.Queue.Abstractions;
using ConversationApi.Infrastructure.Queue.DTO;

namespace ConversationApi.Infrastructure.Queue.Implementations
{
    public class ConversationMessageQueueManager
    {
        private readonly ConversationMessageQueue _conversationMessageQueue;

        public ConversationMessageQueueManager(ConversationMessageQueueFactory conversationMessageQueueFactory)
        {
            _conversationMessageQueue = conversationMessageQueueFactory.Create();
        }

        public ConversationMessageQueueResult Enqueue(ConversationMessageQueueRequest conversationMessageQueueRequest)
        {
            return _conversationMessageQueue.Add(conversationMessageQueueRequest);
        }
    }
}