namespace ConversationApi.Infrastructure.Queue.Abstractions
{
    public abstract class ConversationMessageQueueFactory
    {
        public abstract ConversationMessageQueue Create();
    }
}