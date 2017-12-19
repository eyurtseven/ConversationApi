using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Conversation.ServiceHost.Internal.Controllers
{
    [Route("api/v1")]
    public class ConversationController : Controller
    {
        [HttpPost]
        [Route("conversations")]
        public IActionResult Post()
        {
            throw new NotImplementedException();
        }
    }
}