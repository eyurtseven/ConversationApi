﻿using ConversationApi.Common.ResponseHelper;
using ConversationApi.Model;

namespace ConversationApi.Infrastructure.Repository.Abstractions
{
    public interface IConversationRepository : IRepository<Conversation>
    {
        PaginationResponse<Conversation> GetConversations(long applicationId, long subscriberId, int pageNumber, int pageSize);
    }
}