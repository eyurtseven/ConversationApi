using AutoMapper.QueryableExtensions;
using ConversationApi.Common.ResponseHelper;
using ConversationApi.Infrastructure.Repository.Abstractions;
using ConversationApi.Services.Abstractions;
using ConversationApi.Services.DTO.Request;
using ConversationApi.Services.DTO.Response;

namespace ConversationApi.Services.Implementations
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IConversationMessageRepository _conversationMessageRepository;
        private readonly IUnitOfWork _uow;

        public ConversationService(
            IConversationRepository conversationRepository, 
            IConversationMessageRepository conversationMessageRepository, 
            IUnitOfWork uow)
        {
            _conversationRepository = conversationRepository;
            _conversationMessageRepository = conversationMessageRepository;
            _uow = uow;
        }

        public PaginationResponse<ConversationResponse> GetConversationList(GetConversationListRequest request)
        {
            var conversationList =
                _conversationRepository.GetConversations(
                    request.ApplicationId,
                    request.SubscriberId,
                    request.PageNumber,
                    request.PageSize);
            
            return new PaginationResponse<ConversationResponse>
            {
                Total = conversationList.Total,
                Items = conversationList.Items.ProjectTo<ConversationResponse>()
            };
        }
    }
}