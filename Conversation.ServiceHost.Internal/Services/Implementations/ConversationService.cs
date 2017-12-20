using Conversation.ServiceHost.Internal.Infrastructure.Repository.Abstractions;
using Conversation.ServiceHost.Internal.Services.Abstractions;
using Conversation.ServiceHost.Internal.Services.DTO.Request;
using Conversation.ServiceHost.Internal.Services.DTO.Response;

namespace Conversation.ServiceHost.Internal.Services.Implementations
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository; 

        public ConversationService(
            IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository; 
        }

        public ConversationResponse CreateConversation(PostConversationRequest request)
        {
            throw new System.NotImplementedException();
        }
 
    }
}