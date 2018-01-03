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
            _conversationRepository.Index(new Model.Conversation
            {
                Id = request.ConversationId.ToString(),
                ApplicationId = request.ApplicationId,
                CompanyId = request.CompanyId,
                SenderId = request.SenderId,
                RecipientId = request.RecipientId,
                Subject = request.Subject,
                JobId = request.JobId,
                IsPrivate = request.IsPrivate,
                IsDeleted = false,
                CreateDate = request.CreateDate,
                LastModifyDate = request.LastModifyDate
            });
            
            return new ConversationResponse
            {
                Id = request.ConversationId,
                ApplicationId = request.ApplicationId,
                CompanyId = request.CompanyId,
                SenderId = request.SenderId,
                RecipientId = request.RecipientId,
                Subject = request.Subject,
                JobId = request.JobId,
                IsPrivate = request.IsPrivate,
                IsDeleted = false,
                CreateDate = request.CreateDate,
                LastModifyDate = request.LastModifyDate   
            };
            
        }
 
    }
}