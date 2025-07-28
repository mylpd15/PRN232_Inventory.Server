using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareSync.Business
{
    public class UpdateProductPriceDto : CreateProductPriceDto
    {
        public int ProductPriceId { get; set; }

    }
}
