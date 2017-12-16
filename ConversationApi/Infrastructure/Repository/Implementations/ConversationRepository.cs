using System.Linq;
using ConversationApi.Common.ResponseHelper;
using ConversationApi.Infrastructure.DataContext;
using ConversationApi.Infrastructure.Repository.Abstractions;
using ConversationApi.Model;
using Microsoft.EntityFrameworkCore;

namespace ConversationApi.Infrastructure.Repository.Implementations
{
    public class ConversationRepository : Repository<Conversation>, IConversationRepository
    {
        public ConversationRepository(ConversationContext context) : base(context)
        {
        }

        public PaginationResponse<Conversation> GetConversations(long applicationId, long companyId, long senderId, long recipientId, int pageNumber, int pageSize)
        {
            var conversations = DbSet.AsNoTracking().Where(x => !x.IsDeleted && x.ApplicationId == applicationId && x.CompanyId == companyId && x.SenderId == senderId && x.RecipientId == recipientId).OrderBy(x => x.CreateDate);

            var count = conversations.Count();

            var result = conversations.Skip(pageNumber * pageSize).Take(pageSize);

            return new PaginationResponse<Conversation>
            {
                Total = count,
                Items = result
            };
        }


        public PaginationResponse<Conversation> GetConversationsBySenderId(long applicationId, long companyId, long senderId, int pageNumber, int pageSize)
        {
            var conversations = DbSet.AsNoTracking().Where(x => !x.IsDeleted && x.ApplicationId == applicationId && x.SenderId == senderId && x.CompanyId == companyId).OrderBy(x => x.CreateDate);

            var count = conversations.Count();

            var result = conversations.Skip(pageNumber * pageSize).Take(pageSize);

            return new PaginationResponse<Conversation>
            {
                Total = count,
                Items = result
            };
        }


        public PaginationResponse<Conversation> GetConversationsByCompanyId(long applicationId, long companyId, int pageNumber, int pageSize)
        {
            var conversations = DbSet.AsNoTracking().Where(x => !x.IsDeleted && x.ApplicationId == applicationId && x.CompanyId == companyId).OrderBy(x => x.CreateDate);

            var count = conversations.Count();

            var result = conversations.Skip(pageNumber * pageSize).Take(pageSize);

            return new PaginationResponse<Conversation>
            {
                Total = count,
                Items = result
            };
        }


        public PaginationResponse<Conversation> GetConversationsByRecipientIdAll(long applicationId, long RecipientId, int pageNumber, int pageSize)
        {
            var conversations = DbSet.AsNoTracking().Where(x => !x.IsDeleted && x.ApplicationId == applicationId && x.RecipientId == RecipientId).OrderBy(x => x.CreateDate);

            var count = conversations.Count();

            var result = conversations.Skip(pageNumber * pageSize).Take(pageSize);

            return new PaginationResponse<Conversation>
            {
                Total = count,
                Items = result
            };
        }


        public PaginationResponse<Conversation> GetConversationsByRecipientId(long applicationId, long CompanyId, long RecipientId, int pageNumber, int pageSize)
        {
            var conversations = DbSet.AsNoTracking().Where(x => !x.IsDeleted && x.ApplicationId == applicationId && x.RecipientId == RecipientId && x.CompanyId == CompanyId).OrderBy(x => x.CreateDate);

            var count = conversations.Count();

            var result = conversations.Skip(pageNumber * pageSize).Take(pageSize);

            return new PaginationResponse<Conversation>
            {
                Total = count,
                Items = result
            };
        }

    }
}