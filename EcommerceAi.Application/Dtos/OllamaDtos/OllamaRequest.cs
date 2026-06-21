using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.Dtos.OllamaDtos
{
    public class OllamaRequest
    {
        public string Model { get; set; } = string.Empty;

        public string Prompt { get; set; } = string.Empty;

        public bool Stream { get; set; }
    }
}
