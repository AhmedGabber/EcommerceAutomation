using EcommerceAi.Application.Dtos.CustomerDtos;
using EcommerceAi.Application.Dtos.OrderDtos;
using EcommerceAi.Application.IServices;
using EcommerceAi.Core.Domain_Models;
using EcommerceAi.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            var customers = await _repository.GetAllAsync();

            return customers.Select(x => new CustomerDto
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber
            }).ToList();
        }

        public async Task<CustomerDto?> GetByIdAsync(Guid id)
        {
            var customer = await _repository.GetByIdAsync(id);

            if (customer == null)
                return null;

            return new CustomerDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
        }

        public async Task CreateAsync(CreateCustomerDto dto)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };

            await _repository.AddAsync(customer);
        }

        public async Task UpdateAsync(Guid id, UpdateCustomerDto dto)
        {
            var customer = await _repository.GetByIdAsync(id);

            if (customer == null)
                throw new Exception("Customer not found");

            customer.FullName = dto.FullName;
            customer.Email = dto.Email;
            customer.PhoneNumber = dto.PhoneNumber;

            await _repository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await _repository.GetByIdAsync(id);

            if (customer == null)
                throw new Exception("Customer not found");

            await _repository.DeleteAsync(customer);
        }

        public async Task<List<OrderDto>> GetCustomerOrdersAsync(Guid customerId)
        {
            var customer = await _repository.GetByIdAsync(customerId);

            if (customer == null)
                throw new Exception("Customer not found");

            return customer.Orders.Select(order => new OrderDto
            {
                Id = order.Id,
                CustomerName=order.Customer.FullName,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt
            }).ToList();
        }

        public async Task<CustomerDto?> GetByPhoneNumberAsync(string phoneNumber)
        {
            var customer =
                await _repository.GetByPhoneNumberAsync(
                    phoneNumber);

            if (customer == null)
                return null;

            return new CustomerDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
        }
    }
}
