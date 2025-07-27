using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareSync.Domain;

namespace WareSync.Business
{
    public interface IProductPriceBusiness
    {
        Task<ProductPrice> CreateProductPriceAsync(ProductPrice productPrice);
        Task<ProductPrice> UpdateProductPriceAsync(ProductPrice productPrice);
        Task DeleteProductPriceAsync(int productPriceId);
        Task<ProductPrice?> GetProductPriceByIdAsync(int productPriceId);
        Task<IEnumerable<ProductPrice>> GetAllProductPricesAsync();
    }
}
