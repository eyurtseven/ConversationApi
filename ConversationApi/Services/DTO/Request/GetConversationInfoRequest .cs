namespace ConversationApi.Services.DTO.Request
{
    /// <summary>
    /// Kaynak ve hedef arasındaki konuşma
    /// </summary>
    public class GetConversationInfoRequest : PaginationRequest
    {
        public int CompanyId { get; set; }
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
    }
}