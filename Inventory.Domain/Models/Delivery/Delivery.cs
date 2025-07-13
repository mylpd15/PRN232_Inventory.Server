using System.ComponentModel.DataAnnotations;

namespace Inventory.Domain.Models.Delivery
{
    public class Delivery
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DeliveryStatus deliveryStatus { get; set; } = DeliveryStatus.Pending;
        public string Note { get; set; }

        public Customer Customer { get; set; }
        public ICollection<DeliveryDetail> DeliveryDetails { get; set; }
    }
}