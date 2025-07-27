using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareSync.Domain
{
    public class ProductPrice : AuditableEntity
    {
        public int ProductPriceId { get; set; }
        [ForeignKey("ProductID")]
        public int ProductID { get; set; }
        public decimal CostPrice { get; set; }     // Giá nhập
        public decimal SellingPrice { get; set; }  // Giá bán

        public DateTime EffectiveDate { get; set; } // Ngày bắt đầu có hiệu lực
        public bool IsActive { get; set; }          // Đánh dấu giá hiện tại hay không

        public Product Product { get; set; } = null!;
    }
}
