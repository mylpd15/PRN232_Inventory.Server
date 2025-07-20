using WareSync.Domain;

namespace WareSync.Business;
public interface IProductBusiness
{
    Task<Product> CreateProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task DeleteProductAsync(int productId);
    Task<Product?> GetProductByIdAsync(int productId);
    Task<IEnumerable<Product>> GetAllProductsAsync();
} 