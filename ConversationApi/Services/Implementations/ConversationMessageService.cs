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
    public class ConversationMessageService : IConversationMessageService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IConversationMessageRepository _conversationMessageRepository;
        private readonly IUnitOfWork _uow;

        public ConversationMessageService(IConversationRepository conversationRepository, IConversationMessageRepository conversationMessageRepository, IUnitOfWork uow)
        {
            _conversationRepository = conversationRepository;
            _conversationMessageRepository = conversationMessageRepository;
            _uow = uow;
        }

        public ConversationMessageResponse SendMessage(PostConversationMessageRequest request)
        {
            var queueManager = new ConversationMessageQueueManager(new RabbitMQConversationMessageQueueFactory());

            var conversationId = Guid.NewGuid();
            var creationDate = DateTime.Now;

            queueManager.Enqueue(new ConversationMessageQueueRequest
            {
                ConversationId = conversationId,
                ApplicationId = request.ApplicationId,
                CompanyId = request.CompanyId,
                SenderId = request.SenderId,
                RecipientId = request.RecipientId,
                IsRead = request.IsRead,
                CreationDate = creationDate
            });

            return new ConversationMessageResponse
            {
                Id = conversationId,
                SenderId = request.SenderId,
                RecipientId = request.RecipientId,
                CreateDate = creationDate
            };
        }
    }
}