using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareSync.Business
{
    public class CreateProductPriceDto
    {
        public int ProductID { get; set; }
        public decimal CostPrice { get; set; }     // Giá nhập
        public decimal SellingPrice { get; set; }  // Giá bán
        public DateTime EffectiveDate { get; set; } // Ngày bắt đầu có hiệu lực
        public bool IsActive { get; set; }          // Đánh dấu giá hiện tại hay không
    }
}
