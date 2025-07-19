using WareSync.Domain;

namespace WareSync.Business;
public interface ICustomerBusiness
{
    Task<Domain.Customer> CreateCustomerAsync(Domain.Customer customer);
    Task<Domain.Customer> UpdateCustomerAsync(Domain.Customer customer);
    Task DeleteCustomerAsync(int customerId);
    Task<Domain.Customer?> GetCustomerByIdAsync(int customerId);
    Task<IEnumerable<Domain.Customer>> GetAllCustomersAsync();
} 