
namespace Inventory.Domain.DTOs.DeliveryDto
{
    public class CreateDeliveryDetailDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
