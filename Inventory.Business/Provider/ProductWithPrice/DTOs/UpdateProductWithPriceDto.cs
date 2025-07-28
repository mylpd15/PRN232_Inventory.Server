using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareSync.Business.Provider.ProductWithPrice.DTOs
{
    public class UpdateProductWithPriceDto
    {
        public UpdateProductDto Product { get; set; } = null!;
        public UpdateProductPriceDto ProductPrice { get; set; } = null!;
    }
}
