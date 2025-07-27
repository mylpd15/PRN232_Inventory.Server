using WareSync.Repositories;

namespace WareSync.Business;
public class CustomerBusiness : ICustomerBusiness
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerBusiness(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Domain.Customer> CreateCustomerAsync(Domain.Customer customer)
    {
        // Check if customer name already exists
       /* if (await CustomerNameExistsAsync(customer.CustomerName))
        {
            throw new InvalidOperationException($"Customer with name '{customer.CustomerName}' already exists.");
        }*/
        
        return await _customerRepository.AddAsync(customer);
    }

    public async Task<Domain.Customer> UpdateCustomerAsync(Domain.Customer customer)
    {
        // Check if customer exists
        var existing = await _customerRepository.GetByIdAsync(customer.CustomerID);
        if (existing == null)
        {
            throw new InvalidOperationException($"Customer with ID {customer.CustomerID} does not exist.");
        }

        // Check if customer name already exists (excluding current customer)
        /* if (await CustomerNameExistsAsync(customer.CustomerName, customer.CustomerID))
         {
             throw new InvalidOperationException($"Customer with name '{customer.CustomerName}' already exists.");
         }*/
        existing.CustomerName = customer.CustomerName;
        existing.CustomerAddress = customer.CustomerAddress;

        return await _customerRepository.UpdateAsync(existing);
    }

    public async Task DeleteCustomerAsync(int customerId)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId);
        if (customer == null)
        {
            throw new InvalidOperationException($"Customer with ID {customerId} does not exist.");
        }

        // Check if customer has any deliveries
        if (customer.Deliveries != null && customer.Deliveries.Any())
        {
            throw new InvalidOperationException($"Cannot delete customer with ID {customerId} because they have associated deliveries.");
        }

         _customerRepository.Remove(customer);
        await _customerRepository.SaveChangesAsync();
    }

    public async Task<Domain.Customer?> GetCustomerByIdAsync(int customerId)
    {
        return await _customerRepository.GetByIdAsync(customerId);
    }

    public IQueryable<Domain.Customer> GetAllCustomersAsync()
    {
        return _customerRepository.GetAllCsutomers();
    }

    public async Task<bool> CustomerExistsAsync(int customerId)
    {
        return await _customerRepository.GetByIdAsync(customerId) != null;
    }

    public async Task<bool> CustomerNameExistsAsync(string customerName, int? excludeCustomerId = null)
    {
        var customers = await _customerRepository.GetAllAsync();
        return customers.Any(c => c.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase) 
                                 && (!excludeCustomerId.HasValue || c.CustomerID != excludeCustomerId.Value));
    }

} 