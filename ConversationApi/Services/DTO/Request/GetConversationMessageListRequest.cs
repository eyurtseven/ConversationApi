namespace ConversationApi.Services.DTO.Request
{
    /// <summary>
    /// Mesaj listesi
    /// </summary>
    public class GetConversationMessageListRequest : PaginationRequest
    {
        public string ConversationId { get; set; }
    }
}