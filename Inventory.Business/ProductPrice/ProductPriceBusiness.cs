using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareSync.Domain;
using WareSync.Repositories.ProductRepository;

namespace WareSync.Business
{
    public class ProductPriceBusiness : IProductPriceBusiness
    {
        private readonly IProductPriceRepository _productPriceRepository;
        public ProductPriceBusiness(IProductPriceRepository productPriceRepository)
        {
            _productPriceRepository = productPriceRepository;
        }
        public async Task<ProductPrice> CreateProductPriceAsync(ProductPrice productPrice)
        {
            await _productPriceRepository.AddAsync(productPrice);
            return productPrice;
        }
        public async Task<ProductPrice> UpdateProductPriceAsync(ProductPrice productPrice)
        {
            await _productPriceRepository.UpdateAsync(productPrice);
            return productPrice;
        }
        public async Task DeleteProductPriceAsync(int productPriceId)
        {
            var productPrice = await _productPriceRepository.GetByIdAsync(productPriceId);
            if (productPrice != null)
                _productPriceRepository.Remove(productPrice);
        }
        public async Task<ProductPrice?> GetProductPriceByIdAsync(int productId)
        {
            return await _productPriceRepository.GetByIdAsync(productId);
        }
        public async Task<IEnumerable<ProductPrice>> GetAllProductPricesAsync()
        {
            return await _productPriceRepository.GetAllAsync();
        }
    }
}
