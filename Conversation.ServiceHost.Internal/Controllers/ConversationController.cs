using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conversation.ServiceHost.Internal.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Conversation.ServiceHost.Internal.Controllers
{
    [Route("api/v1")]
    public class ConversationController : Controller
    {
        
        private readonly IConversationService _conversationService;

        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }
        
        [HttpPost]
        [Route("conversations")]
        public IActionResult Post()
        {
            throw new NotImplementedException();
        }
    }
}