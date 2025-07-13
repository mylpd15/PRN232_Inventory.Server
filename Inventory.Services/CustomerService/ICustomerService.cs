
using Inventory.Domain;
using Inventory.Domain.DTOs.CustomerDto;

namespace Inventory.Services
{
    public interface ICustomerService
    {
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto);
        Task<CustomerDto> UpdateCustomerAsync(Guid id, UpdateCustomerDto dto);
        Task<bool> DeleteCustomerAsync(Guid id);
        Task<CustomerDto> GetCustomerByIdAsync(Guid id);
        Task<List<CustomerDto>> GetAllCustomersAsync();
    }
}