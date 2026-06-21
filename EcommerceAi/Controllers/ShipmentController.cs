using EcommerceAi.Application.Dtos.ShipmentDtos;
using EcommerceAi.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _service;

        public ShipmentController(
            IShipmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result =
                await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetByOrderId(
            Guid orderId)
        {
            var result =
                await _service.GetByOrderIdAsync(orderId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateShipmentDto dto)
        {
            await _service.CreateAsync(dto);

            return Ok();
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(
            Guid id,
            UpdateShipmentStatusDto dto)
        {
            await _service.UpdateStatusAsync(
                id,
                dto.Status);

            return Ok();
        }

        [HttpPut("{id}/delay")]
        public async Task<IActionResult> Delay(
            Guid id,
            DelayShipmentDto dto)
        {
            await _service.MarkAsDelayedAsync(
                id,
                dto.Reason);

            return Ok();
        }
    }
}
