using System;

namespace ConversationApi.Services.DTO.Response
{
    public class ConversationMessageResponse
    {
        public Guid Id { get; set; }
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
        public string Subject { get; set; }
        public DateTime CreateDate { get; set; }
    }
}