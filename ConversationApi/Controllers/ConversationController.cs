﻿using System;
using ConversationApi.Services.Abstractions;
using ConversationApi.Services.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace ConversationApi.Controllers
{
    [Route("api/v1")]
    public class ConversationController : ApiController
    {
        private readonly IConversationService _conversationService;

        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpGet]
        [Route("conversations")]
        public IActionResult Get(GetConversationListRequest request)
        {
            var serviceResponse = _conversationService.GetConversationList(request);
            return ApiResponse(serviceResponse);
        }

        [HttpPost]
        [Route("conversations")]
        public IActionResult Post(PostConversationRequest request)
        {
            var serviceResponse = _conversationService.CreateConversation(request);
            return ApiResponse(serviceResponse);
        }
        
    }
}