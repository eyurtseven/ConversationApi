using System;
using System.Collections.Generic;
using Conversation.ServiceHost.Internal.Infrastructure.Repository.Abstractions;
using Nest;

namespace Conversation.ServiceHost.Internal.Model
{
    [ElasticsearchType(Name = "conversation")]
    public class Conversation : INoSqlEntity
    {
        public string Id { get; set; }
        public string ParentId { get; }
        public string GrandParentId { get; }
        
        public long ApplicationId { get; set; }
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
        public string JobId { get; set; }
        public string Subject { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPrivate { get; set; }
        public long CompanyId { get; set; }

        public ICollection<ConversationMessage> ConversationMessages { get; set; }
    }
}