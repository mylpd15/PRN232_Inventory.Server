using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareSync.Business.Provider.ProductWithPrice.DTOs
{
    public class CreateProductWithPriceDto
    {
        public CreateProductDto Product { get; set; } = null!;
        public CreateProductPriceDto ProductPrice { get; set; } = null!;
    }
}
