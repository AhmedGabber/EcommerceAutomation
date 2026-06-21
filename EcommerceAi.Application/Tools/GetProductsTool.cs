using EcommerceAi.Application.AI;
using EcommerceAi.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.Tools
{
    public class GetProductsTool : IAIServiceTool
    {
        private readonly IProductService _productService;

        public GetProductsTool(IProductService productService)
        {
            _productService = productService;
        }

        public string Name => "GetProducts";

        public async Task<object> ExecuteAsync(Dictionary<string, object>? args)
        {
            var products = await _productService.GetAllAsync();

            return new
            {
                count = products.Count,
                products
            };
        }
    }
}
