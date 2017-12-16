namespace ConversationApi.Services.DTO.Request
{
    /// <summary>
    /// Adayın ilgili firma ile bütün konuşmaları
    /// </summary>
    public class GetConversationListByRecipientIdRequest : PaginationRequest
    {
        public int CompanyId { get; set; }
        public long RecipientId { get; set; }
    }
}