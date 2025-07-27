using WareSync.Domain;

namespace WareSync.Repositories.ProductRepository;
public class ProductPriceRepository : GenericRepository<ProductPrice>, IProductPriceRepository
{
    public ProductPriceRepository(DataContext context) : base(context) { }
    // Thêm các method đặc thù nếu cần
} 