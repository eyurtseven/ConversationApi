using System;
using ConversationApi.Services.Abstractions;
using ConversationApi.Services.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace ConversationApi.Controllers
{
    [Route("api/v1")]
    public class ConversationMessageController : ApiController
    {
        private readonly IConversationMessageService _conversationMessageService;

        public ConversationMessageController(IConversationMessageService conversationMessageService)
        {
            _conversationMessageService = conversationMessageService;
        }

        [HttpPost]
        [Route("conversationmessages")]
        public IActionResult Post(PostConversationMessageRequest request)
        {
            var serviceResponse = _conversationMessageService.SendMessage(request);
            return ApiResponse(serviceResponse);
        }
    }
}