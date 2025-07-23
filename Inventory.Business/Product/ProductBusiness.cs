using WareSync.Domain;
using WareSync.Repositories.ProductRepository;

namespace WareSync.Business;
public class ProductBusiness : IProductBusiness
{
    private readonly IProductRepository _productRepository;
    public ProductBusiness(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Product> CreateProductAsync(Product product)
    {
        await _productRepository.AddAsync(product);
        return product;
    }
    public async Task<Product> UpdateProductAsync(Product product)
    {
        await _productRepository.UpdateAsync(product);
        return product;
    }
    public async Task DeleteProductAsync(int productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product != null)
            _productRepository.Remove(product);
    }
    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _productRepository.GetByIdAsync(productId);
    }
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }
} 