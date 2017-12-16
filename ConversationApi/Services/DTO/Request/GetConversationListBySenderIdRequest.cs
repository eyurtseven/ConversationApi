namespace ConversationApi.Services.DTO.Request
{
    /// <summary>
    /// İligili şirkete ait sadece iligli kişinin konuşmaları
    /// </summary>
    public class GetConversationListBySenderIdRequest : PaginationRequest
    {
        public int CompanyId { get; set; }
        public long SenderId { get; set; }
    }
}