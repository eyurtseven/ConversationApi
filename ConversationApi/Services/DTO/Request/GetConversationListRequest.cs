namespace ConversationApi.Services.DTO.Request
{
    public class GetConversationListRequest : PaginationRequest, IServiceRequest
    {
        public long ApplicationId { get; set; }
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
    }
}