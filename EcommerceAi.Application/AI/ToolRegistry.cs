using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.AI
{
    public class ToolRegistry : IToolRegistry
    {
        private readonly IEnumerable<IAIServiceTool> _tools;

        public ToolRegistry(IEnumerable<IAIServiceTool> tools)
        {
            _tools = tools;
        }

        public IAIServiceTool GetTool(string name)
        {
            return _tools.First(t => t.Name == name);
        }
    }
}
