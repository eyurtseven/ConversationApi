using ConversationApi.Infrastructure.ExceptionHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ConversationApi.Infrastructure.Filter
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException ex)
            {
                context.Exception = null;
                var apiError = new ApiError
                {
                    Success = false,
                    Errors = ex.Errors
                };
                context.HttpContext.Response.StatusCode = ex.StatusCode;
                context.Result = new JsonResult(apiError, new JsonSerializerSettings {Formatting = Formatting.Indented});
            }
            else
            { 
                context.Result = new JsonResult(context.Exception, new JsonSerializerSettings {Formatting = Formatting.Indented});
                context.HttpContext.Response.StatusCode = 500;
            } 
            base.OnException(context);
        }
    }
}