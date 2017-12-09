namespace ConversationApi.Services.DTO.Request
{
    public class PostConversationRequest : ServiceRequest
    {
        public long ApplicationId { get; set; }
        public long CompanyId { get; set; }
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
        public string JobId { get; set; }
        public string Subject { get; set; }
        public bool IsPrivate { get; set; }
    }
}