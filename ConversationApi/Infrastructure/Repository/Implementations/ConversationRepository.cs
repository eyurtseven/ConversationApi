using System.Linq;
using ConversationApi.Common.ResponseHelper;
using ConversationApi.Infrastructure.DataContext;
using ConversationApi.Infrastructure.Repository.Abstractions;
using ConversationApi.Model;
using Microsoft.EntityFrameworkCore;

namespace ConversationApi.Infrastructure.Repository.Implementations
{
    public class ConversationRepository : Repository<Conversation>, IConversationRepository
    {
        public ConversationRepository(ConversationContext context) : base(context)
        {
        }

        public PaginationResponse<Conversation> GetConversations(
            long applicationId, 
            long senderId, 
            long recipientId, 
            int pageNumber,
            int pageSize)
        {
            var conversations = DbSet.AsNoTracking()
                .Where(x =>
                    !x.IsDeleted &&
                    x.ApplicationId == applicationId &&
                    x.SenderId == senderId && 
                    x.RecipientId == recipientId)
                .OrderBy(x => x.CreateDate);
            
            var count = conversations.Count();

            var result = conversations.Skip(pageNumber * pageSize).Take(pageSize);

            return new PaginationResponse<Conversation>
            {
                Total = count,
                Items = result
            };
        }
    }
}