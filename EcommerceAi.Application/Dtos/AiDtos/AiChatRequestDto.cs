using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.Dtos.AiDtos
{
    public class AiChatRequestDto
    {
        public string Message { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
