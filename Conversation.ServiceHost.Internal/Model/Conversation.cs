using System;
using System.Collections.Generic;

namespace Conversation.ServiceHost.Internal.Model
{
    public class Conversation
    {
        public Guid Id { get; set; }
        public int ApplicationId { get; set; }
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
        public string Subject { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public bool IsDeleted { get; set; }
        public int CompanyId { get; set; }

        public ICollection<ConversationMessage> ConversationMessages { get; set; }

    }
}