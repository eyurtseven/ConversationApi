namespace ConversationApi.Services.DTO.Request
{
    public class GetConversationListRequest : PaginationRequest
    {
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
        public long CompanyId { get; set; }
    }
}