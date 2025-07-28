using System.ComponentModel.DataAnnotations;

namespace WareSync.Api.DTOs
{
    public class ProductPriceDto
    {
        [Key]
        public int ProductPriceId { get; set; }

        public int ProductID { get; set; }
        public decimal CostPrice { get; set; }     // Giá nhập
        public decimal SellingPrice { get; set; }  // Giá bán

        public DateTime EffectiveDate { get; set; } // Ngày bắt đầu có hiệu lực
        public bool IsActive { get; set; }          // Đánh dấu giá hiện tại hay không

    }
}
