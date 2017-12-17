using System;

namespace ConversationApi.Infrastructure.Queue.DTO
{
    public class ConversationQueueRequest
    {
        public Guid ConversationId { get; set; }
        public long ApplicationId { get; set; }
        public long CompanyId { get; set; }
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
        public string JobId { get; set; }
        public string Subject { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime CreationDate { get; set; }
    }
}