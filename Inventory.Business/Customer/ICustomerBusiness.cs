using WareSync.Domain;

namespace WareSync.Business;
public interface ICustomerBusiness
{
    Task<Domain.Customer> CreateCustomerAsync(Domain.Customer customer);
    Task<Domain.Customer> UpdateCustomerAsync(Domain.Customer customer);
    Task DeleteCustomerAsync(int customerId);
    Task<Domain.Customer?> GetCustomerByIdAsync(int customerId);
    IQueryable<Domain.Customer> GetAllCustomersAsync();
    Task<bool> CustomerExistsAsync(int customerId);
    Task<bool> CustomerNameExistsAsync(string customerName, int? excludeCustomerId = null);
} 