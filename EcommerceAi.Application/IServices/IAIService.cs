using EcommerceAi.Application.Dtos.AiDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.IServices
{
    public interface IAIService
    {
        Task<AiChatResponseDto> ChatAsync(AiChatRequestDto request);
    }
}
