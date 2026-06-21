using EcommerceAi.Application.Dtos.InventoryDtos;
using EcommerceAi.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        [HttpGet("getAllInventory")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("getInventory/{productId}")]
        public async Task<IActionResult> GetByProductId(Guid productId)
        {
            var result = await _service.GetByProductIdAsync(productId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("updateInventory/{productId}")]
        public async Task<IActionResult> Update(
            Guid productId,
            UpdateInventoryDto dto)
        {
            await _service.UpdateAsync(productId, dto);

            return Ok();
        }
    }
}
