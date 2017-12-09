using ConversationApi.Infrastructure.Queue.Abstractions;
using ConversationApi.Infrastructure.Queue.DTO;

namespace ConversationApi.Infrastructure.Queue.Implementations
{
    public class ConversationQueueManager
    {
        private readonly ConversationQueue _conversationQueue;

        public ConversationQueueManager(ConversationQueueFactory conversationQueueFactory)
        {
            _conversationQueue = conversationQueueFactory.Create();
        }

        public ConversationQueueResult Enqueue(ConversationQueueRequest conversationQueueRequest)
        {
            return _conversationQueue.Add(conversationQueueRequest);
        }
    }
}