namespace ConversationApi.Services.DTO.Request
{
    public class PaginationRequest : ServiceRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}