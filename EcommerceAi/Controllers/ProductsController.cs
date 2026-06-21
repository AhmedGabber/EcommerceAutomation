using EcommerceAi.Application.Dtos.ProductDtos;
using EcommerceAi.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("getAllProducts")]

        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("getproduct/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("createProduct")]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            await _service.CreateAsync(dto);

            return Ok();
        }

        [HttpPut("updateProduct/{id}")]
        public async Task<IActionResult> Update(Guid id, CreateProductDto dto)
        {
            await _service.UpdateAsync(id, dto);

            return Ok();
        }

        [HttpDelete("deleteProduct/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);

            return Ok();
        }
    }
}
