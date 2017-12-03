namespace ConversationApi.Infrastructure.ExceptionHandling
{
    public class ApiErrorDetail
    {
        public ApiErrorDetail()
        {
            
        }
        public ApiErrorDetail(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; set; }
        public string Message { get; set; }
    }
}