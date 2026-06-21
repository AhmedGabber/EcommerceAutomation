using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.Dtos.ToolsDto
{
    public class ToolCallDto
    {
        public string Tool { get; set; }

        public Dictionary<string, object> Args { get; set; }
    }
}
