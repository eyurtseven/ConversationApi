using System;

namespace ConversationApi.Infrastructure.Queue.DTO
{
    public class ConversationMessageQueueRequest
    {
        public Guid ConversationId { get; set; }
        public long ApplicationId { get; set; }
        public long CompanyId { get; set; }
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreationDate { get; set; }
    }
}