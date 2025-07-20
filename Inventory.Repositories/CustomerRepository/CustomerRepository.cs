using WareSync.Domain;

namespace WareSync.Repositories;
public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(DataContext context) : base(context) { }
    // Thêm các method đặc thù nếu cần
} 