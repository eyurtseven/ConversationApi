using System;

namespace ConversationApi.Services.DTO.Response
{
    public class ConversationResponse
    {
        public Guid Id { get; set; }
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        public string Subject { get; set; }
        public DateTime CreateDate { get; set; }
    }
}