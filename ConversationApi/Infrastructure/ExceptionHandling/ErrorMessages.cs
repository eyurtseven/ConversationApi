namespace ConversationApi.Infrastructure.ExceptionHandling
{
    public static class ErrorMessages
    {
     
        public const int NotImplementedYet = 100;

        private const string ErrorPrefix = "ERR_";
        public static string GetErrorCode(this int errorNo) => $"{ErrorPrefix}{errorNo}";

    }
}