namespace ConversationApi.Services.DTO.Request
{
    /// <summary>
    /// Adayın bütün konuşmaları
    /// </summary>
    public class GetConversationListByRecipientIdAllRequest : PaginationRequest
    {
        public long RecipientId { get; set; }
    }
}