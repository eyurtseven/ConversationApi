using Microsoft.AspNetCore.Mvc;

namespace ConversationApi.Controllers
{
    [Route("")]
    [ApiExplorerSettings(IgnoreApi=true)]
    public class HomeController : ApiController
    { 
        [HttpGet]
        [Route("")]
        [ApiExplorerSettings(IgnoreApi=true)]
        public IActionResult Get()
        { 
            return Redirect("swagger");
        }
    }
}