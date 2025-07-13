
using Inventory.Domain.DTOs.DeliveryDto;

namespace Inventory.Domain
{
    public class CreateDeliveryDto
    {
        public Guid CustomerId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Note { get; set; }
        public List<CreateDeliveryDetailDto> DeliveryDetails { get; set; }
    }

    public class UpdateDeliveryDto
    {
        public Guid CustomerId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public string Note { get; set; }
        public List<UpdateDeliveryDetailDto> DeliveryDetails { get; set; }
    }

    public class DeliveryDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public string Note { get; set; }
        public CustomerDto Customer { get; set; }
        public List<DeliveryDetailDto> DeliveryDetails { get; set; }
    }
}