using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.AI
{
    public interface IAIServiceTool
    {
        string Name { get; }

        Task<object> ExecuteAsync(Dictionary<string, object>? args);
    }

}
