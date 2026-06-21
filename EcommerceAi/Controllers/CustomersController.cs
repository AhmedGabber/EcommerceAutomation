using EcommerceAi.Application.Dtos.CustomerDtos;
using EcommerceAi.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet("getAllCustomers")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("getCustomer/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("createCustomer")]
        public async Task<IActionResult> Create(CreateCustomerDto dto)
        {
            await _service.CreateAsync(dto);

            return Ok();
        }

        [HttpPut("updateCustomer/{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateCustomerDto dto)
        {
            await _service.UpdateAsync(id, dto);

            return Ok();
        }

        [HttpDelete("deleteCustomer{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);

            return Ok();
        }

        [HttpGet("customerOrder/{id}")]
        public async Task<IActionResult> GetOrders(Guid id)
        {
            return Ok(await _service.GetCustomerOrdersAsync(id));
        }
    }
}
