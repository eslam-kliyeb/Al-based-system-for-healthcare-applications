using AI.Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace AI.Controllers
{
    public class ChatBotController : APIBaseController
    {
        private readonly IChatBotService _chatBotService;
        public ChatBotController(IChatBotService chatBotService)
        {
            _chatBotService = chatBotService;
        }
        [HttpPost("ChatBot")]
        public async Task<string> Chatbot(string input)
        {
            string x = await _chatBotService.ChatBot(input);
            return x;
        }
    }
}
