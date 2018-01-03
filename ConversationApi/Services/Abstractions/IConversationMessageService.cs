using ConversationApi.Common.ResponseHelper;
using ConversationApi.Services.DTO.Request;
using ConversationApi.Services.DTO.Response;

namespace ConversationApi.Services.Abstractions
{
    public interface IConversationMessageService
    {
        ConversationMessageResponse SendMessage(PostConversationMessageRequest request);
    }
}