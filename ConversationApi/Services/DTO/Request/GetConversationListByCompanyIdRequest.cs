namespace ConversationApi.Services.DTO.Request
{
    /// <summary>
    /// İlgili şirkete ait bütün konuşmaları
    /// </summary>
    public class GetConversationListByCompanyIdRequest : PaginationRequest
    {
        public int CompanyId { get; set; }
    }
}