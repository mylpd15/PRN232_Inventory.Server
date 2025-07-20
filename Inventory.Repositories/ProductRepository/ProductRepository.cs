using WareSync.Domain;

namespace WareSync.Repositories.ProductRepository;
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(DataContext context) : base(context) { }
    // Thêm các method đặc thù nếu cần
} 