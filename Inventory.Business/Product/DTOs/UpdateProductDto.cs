using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareSync.Business
{
    public class UpdateProductDto : CreateProductDto
    {
        public int ProductID { get; set; }
    }
}
