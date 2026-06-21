using EcommerceAi.Application.Dtos.OrderDtos;
using EcommerceAi.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet("getAllOrders")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("getOrder/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("createOrder")]
        public async Task<IActionResult> Create(CreateOrderDto dto)
        {
            await _service.CreateAsync(dto);

            return Ok();
        }

        [HttpPut("updateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(
            Guid id,
            UpdateOrderStatusDto dto)
        {
            await _service.UpdateStatusAsync(id, dto.Status);

            return Ok();
        }

        [HttpDelete("deleteOrder/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);

            return Ok();
        }

        [HttpPut("updateOrder/{id}")]
        public async Task<IActionResult> Update(Guid id,UpdateOrderDto dto)
        {
            await _service.UpdateAsync(id, dto);

            return Ok();
        }
    }
}
