using Microsoft.EntityFrameworkCore;
using WareSync.Domain;
using WareSync.Repositories.ProductRepository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WareSync.Business;
public class ProductBusiness : IProductBusiness
{
    private readonly IProductRepository _productRepository;
    private readonly IProductPriceBusiness _priceBusiness;
    public ProductBusiness(
             IProductRepository productRepository,
             IProductPriceBusiness priceBusiness
             )
    {
        _productRepository = productRepository;
        _priceBusiness = priceBusiness;
    }
    public async Task<Product> CreateProductWithPriceAsync(Product product, ProductPrice price)
    {
        var createdProduct = await _productRepository.AddAsync(product);

        price.ProductID = createdProduct.ProductID;

        await _priceBusiness.CreateProductPriceAsync(price);

        // 4. (Tuỳ chọn) Đổ navigation collection để trả về
        createdProduct.Prices = new List<ProductPrice> { price };

        return createdProduct;
    }
    //public async Task<Product> CreateProductAsync(Product product)
    //{
    //    await _productRepository.AddAsync(product);
    //    return product;
    //}
    public async Task<Product> UpdateProductWithPriceAsync(Product product, ProductPrice price)
    {
        var createdProduct = await _productRepository.UpdateAsync(product);

        price.ProductID = product.ProductID;

        if (price.ProductPriceId > 0)
        {
            await _priceBusiness.UpdateProductPriceAsync(price);
        }
        else
        {
            await _priceBusiness.CreateProductPriceAsync(price);
        }


        var updated = await GetProductByIdAsync(product.ProductID);

        return updated;
    }

    //public async Task<Product> UpdateProductAsync(Product product)
    //{
    //    await _productRepository.UpdateAsync(product);
    //    return product;
    //}
    public async Task DeleteProductAsync(int productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product != null)
            await _productRepository.Remove(product);
    }
    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _productRepository
                .Query()
                .Include(p => p.Prices)
                .FirstOrDefaultAsync(p => p.ProductID == productId);
    }
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository
                   .Query()                                  
                   .Include(p => p.Prices)// load luôn Prices
                   .ToListAsync();
    }
} 