using EcommerceAi.Application.Dtos.AiDtos;
using EcommerceAi.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAi.API.Controllers
{
    [ApiController]
    [Route("api/ai")]
    public class AIController : ControllerBase
    {
        private readonly IAIService _aiService;

        public AIController(
            IAIService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat(
            AiChatRequestDto request)
        {
            var result =
                await _aiService.ChatAsync(request);

            return Ok(result);
        }
    }
}
