namespace ConversationApi.Services.DTO.Request
{
    public class GetConversationListRequest : PaginationRequest, IServiceRequest
    {
        public long ApplicationId { get; set; }
        public long SubscriberId { get; set; }
    }
}