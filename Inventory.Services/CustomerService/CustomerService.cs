
using Inventory.Domain;
using Inventory.Domain.DTOs.CustomerDto;

namespace Inventory.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly List<CustomerDto> _customers = new();

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto)
        {
            var customer = new CustomerDto
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Address = dto.Address,
                Phone = dto.Phone,
                Email = dto.Email
            };
            _customers.Add(customer);
            return await Task.FromResult(customer);
        }

        public async Task<CustomerDto> UpdateCustomerAsync(Guid id, UpdateCustomerDto dto)
        {
            var customer = _customers.FirstOrDefault(x => x.Id == id);
            if (customer == null) return null;
            customer.Name = dto.Name;
            customer.Address = dto.Address;
            customer.Phone = dto.Phone;
            customer.Email = dto.Email;
            return await Task.FromResult(customer);
        }

        public async Task<bool> DeleteCustomerAsync(Guid id)
        {
            var customer = _customers.FirstOrDefault(x => x.Id == id);
            if (customer == null) return false;
            _customers.Remove(customer);
            return await Task.FromResult(true);
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(Guid id)
        {
            var customer = _customers.FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(customer);
        }

        public async Task<List<CustomerDto>> GetAllCustomersAsync()
        {
            return await Task.FromResult(_customers.ToList());
        }
    }
}