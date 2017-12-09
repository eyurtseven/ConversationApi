namespace ConversationApi.Infrastructure.Queue.Abstractions
{
    public abstract class ConversationQueueFactory
    {
        public abstract ConversationQueue Create();
    }
}