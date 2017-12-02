using ConversationApi.Infrastructure.DataContext;
using ConversationApi.Infrastructure.Repository.Abstractions;
using ConversationApi.Model;

namespace ConversationApi.Infrastructure.Repository.Implementations
{
    public class ConversationMessageRepository : Repository<ConversationMessage>, IConversationMessageRepository
    {
        public ConversationMessageRepository(ConversationContext context) : base(context)
        {
        }
    }
}