using System.Collections.Generic;

namespace ConversationApi.Infrastructure.ExceptionHandling
{
    public class ApiError
    {
        public bool Success { get; set; }
        public List<ApiErrorDetail> Errors { get; set; }
    }
}