using WareSync.Domain;

namespace WareSync.Business;
public interface IProductBusiness
{
    Task<Product> CreateProductWithPriceAsync(Product product, ProductPrice price);
    //Task<Product> CreateProductAsync(Product product);
    Task<Product> UpdateProductWithPriceAsync(Product product, ProductPrice price);
    //Task<Product> UpdateProductAsync(Product product);
    Task DeleteProductAsync(int productId);
    Task<Product?> GetProductByIdAsync(int productId);
    Task<IEnumerable<Product>> GetAllProductsAsync();
} 