using System;

namespace Conversation.ServiceHost.Internal.Services.DTO.Request
{
    public class PostConversationRequest : ServiceRequest
    {
        public Guid ConversationId { get; set; }
        public long ApplicationId { get; set; }
        public long CompanyId { get; set; }
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
        public string JobId { get; set; }
        public string Subject { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifyDate { get; set; }
    }
}