using ConversationApi.Common.ResponseHelper;
using ConversationApi.Model;

namespace ConversationApi.Infrastructure.Repository.Abstractions
{
    public interface IConversationRepository : IRepository<Conversation>
    {
        PaginationResponse<Conversation> GetConversations(long applicationId, long companyId, long senderId, long recipientId, int pageNumber, int pageSize);

        PaginationResponse<Conversation> GetConversationsBySenderId(long applicationId, long companyId, long senderId, int pageNumber, int pageSize);

        PaginationResponse<Conversation> GetConversationsByCompanyId(long applicationId, long companyId, int pageNumber, int pageSize);

        PaginationResponse<Conversation> GetConversationsByRecipientIdAll(long applicationId, long RecipientId, int pageNumber, int pageSize);

        PaginationResponse<Conversation> GetConversationsByRecipientId(long applicationId, long CompanyId, long RecipientId, int pageNumber, int pageSize);
    }
}