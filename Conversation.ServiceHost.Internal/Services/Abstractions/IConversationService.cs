using Conversation.ServiceHost.Internal.Services.DTO.Request;
using Conversation.ServiceHost.Internal.Services.DTO.Response;

namespace Conversation.ServiceHost.Internal.Services.Abstractions
{
    public interface IConversationService
    { 
        ConversationResponse CreateConversation(PostConversationRequest request);
    }
}