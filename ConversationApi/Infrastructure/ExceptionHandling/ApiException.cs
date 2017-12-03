using System;
using System.Collections.Generic;

namespace ConversationApi.Infrastructure.ExceptionHandling
{
    public class ApiException : Exception
    {
        public int StatusCode { get; } = 400;

        public List<ApiErrorDetail> Errors { get; set; }

        public ApiException()
        {
            if (Errors == null)
            {
                Errors = new List<ApiErrorDetail>();
            }
        }

        public ApiException(ApiErrorDetail detail)
        {
            if (Errors == null)
            {
                Errors = new List<ApiErrorDetail>();
            }
            Errors.Add(detail);
        }
    }
}