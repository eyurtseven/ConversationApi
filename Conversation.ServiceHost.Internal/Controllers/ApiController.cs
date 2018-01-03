using Microsoft.AspNetCore.Mvc;

namespace Conversation.ServiceHost.Internal.Controllers
{
    public class ApiController : ControllerBase
    {
        protected IActionResult ApiResponse(object result = null)
        {
            return Ok(new
            {
                Success = true,
                Data = result
            });
        }
    }
}