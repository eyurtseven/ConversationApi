namespace ConversationApi.Services.DTO.Request
{
    public class PostConversationMessageRequest : ServiceRequest
    {
        public long ApplicationId { get; set; }
        public long CompanyId { get; set; }
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
        public bool IsRead { get; set; }
    }
}