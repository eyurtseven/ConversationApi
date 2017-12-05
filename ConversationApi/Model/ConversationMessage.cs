using System;

namespace ConversationApi.Model
{
    public class ConversationMessage
    {

        public Guid Id { get; set; }
        public long SenderId { get; set; }
        public string SenderAvatar { get; set; }
        public long RecipientId { get; set; }
        public string RecipientAvatar { get; set; }
        public string Message { get; set; }
        public DateTime? ReadDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public bool IsDeleted { get; set; }

        public Guid ConversationId { get; set; }
        public Conversation Conversation { get; set; }
    }
}