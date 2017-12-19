using System;
using AutoMapper.QueryableExtensions;
using ConversationApi.Common.ResponseHelper;
using ConversationApi.Infrastructure.Queue.DTO;
using ConversationApi.Infrastructure.Queue.Implementations;
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

        public ConversationService(IConversationRepository conversationRepository, IConversationMessageRepository conversationMessageRepository, IUnitOfWork uow)
        {
            _conversationRepository = conversationRepository;
            _conversationMessageRepository = conversationMessageRepository;
            _uow = uow;
        }

        public PaginationResponse<ConversationResponse> GetConversationList(GetConversationListRequest request)
        {
            var conversationList = _conversationRepository.GetConversations(request.ApplicationId, request.CompanyId, request.SenderId, request.RecipientId, request.PageNumber, request.PageSize);

            return new PaginationResponse<ConversationResponse>
            {
                Total = conversationList.Total,
                Items = conversationList.Items.ProjectTo<ConversationResponse>()
            };
        }

        public PaginationResponse<ConversationResponse> GetConversationBySenderIdList(GetConversationListBySenderIdRequest request)
        {
            var conversationList = _conversationRepository.GetConversationsBySenderId(request.ApplicationId, request.CompanyId, request.SenderId, request.PageNumber, request.PageSize);

            return new PaginationResponse<ConversationResponse>
            {
                Total = conversationList.Total,
                Items = conversationList.Items.ProjectTo<ConversationResponse>()
            };
        }

        public PaginationResponse<ConversationResponse> GetConversationByCompanyIdList(GetConversationListByCompanyIdRequest request)
        {
            var conversationList = _conversationRepository.GetConversationsByCompanyId(request.ApplicationId, request.CompanyId, request.PageNumber, request.PageSize);

            return new PaginationResponse<ConversationResponse>
            {
                Total = conversationList.Total,
                Items = conversationList.Items.ProjectTo<ConversationResponse>()
            };
        }

        public PaginationResponse<ConversationResponse> GetConversationByRecipientIdAllList(GetConversationListByRecipientIdAllRequest request)
        {
            var conversationList = _conversationRepository.GetConversationsByRecipientIdAll(request.ApplicationId, request.RecipientId, request.PageNumber, request.PageSize);

            return new PaginationResponse<ConversationResponse>
            {
                Total = conversationList.Total,
                Items = conversationList.Items.ProjectTo<ConversationResponse>()
            };
        }

        public PaginationResponse<ConversationResponse> GetConversationByRecipientIdList(GetConversationListByRecipientIdRequest request)
        {
            var conversationList = _conversationRepository.GetConversationsByRecipientId(request.ApplicationId, request.CompanyId, request.RecipientId, request.PageNumber, request.PageSize);

            return new PaginationResponse<ConversationResponse>
            {
                Total = conversationList.Total,
                Items = conversationList.Items.ProjectTo<ConversationResponse>()
            };
        }

        public ConversationResponse CreateConversation(PostConversationRequest request)
        {
            var queueManager = new ConversationQueueManager(new RabbitMQConversationQueueFactory());

            var conversationId = Guid.NewGuid();
            var creationDate = DateTime.Now;

            queueManager.Enqueue(new ConversationQueueRequest
            {
                ConversationId = conversationId,
                ApplicationId = request.ApplicationId,
                CompanyId = request.CompanyId,
                SenderId = request.SenderId,
                RecipientId = request.RecipientId,
                JobId = request.JobId,
                Subject = request.Subject,
                IsPrivate = request.IsPrivate,
                CreationDate = creationDate
            });

            return new ConversationResponse
            {
                Id = conversationId,
                SenderId = request.SenderId,
                RecipientId = request.RecipientId,
                Subject = request.Subject,
                CreateDate = creationDate
            };
        }
    }
}