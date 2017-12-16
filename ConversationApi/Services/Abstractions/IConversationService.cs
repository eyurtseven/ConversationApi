using ConversationApi.Common.ResponseHelper;
using ConversationApi.Services.DTO.Request;
using ConversationApi.Services.DTO.Response;

namespace ConversationApi.Services.Abstractions
{
    public interface IConversationService
    {
        PaginationResponse<ConversationResponse> GetConversationList(GetConversationListRequest request);
        PaginationResponse<ConversationResponse> GetConversationBySenderIdList(GetConversationListBySenderIdRequest request);
        PaginationResponse<ConversationResponse> GetConversationByCompanyIdList(GetConversationListByCompanyIdRequest request);
        PaginationResponse<ConversationResponse> GetConversationByRecipientIdAllList(GetConversationListByRecipientIdAllRequest request);
        PaginationResponse<ConversationResponse> GetConversationByRecipientIdList(GetConversationListByRecipientIdRequest request);

        ConversationResponse CreateConversation(PostConversationRequest request);
    }
}