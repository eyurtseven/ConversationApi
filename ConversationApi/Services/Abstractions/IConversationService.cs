using ConversationApi.Common.ResponseHelper;
using ConversationApi.Services.DTO.Request;
using ConversationApi.Services.DTO.Response;

namespace ConversationApi.Services.Abstractions
{
    public interface IConversationService
    {
        PaginationResponse<ConversationResponse> GetConversationList(GetConversationListRequest request);
        ConversationResponse CreateConversation(PostConversationRequest request);
    }
}