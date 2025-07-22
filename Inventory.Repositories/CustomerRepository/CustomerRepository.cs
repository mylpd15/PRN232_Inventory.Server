using WareSync.Domain;

namespace WareSync.Repositories;
public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    private readonly DataContext _context;
    public CustomerRepository(DataContext context) : base(context) { _context = context; }

    public IQueryable<Customer> GetAllCsutomers()
    {
        return _context.Customers;
    }
    // Thêm các method đặc thù nếu cần
} 