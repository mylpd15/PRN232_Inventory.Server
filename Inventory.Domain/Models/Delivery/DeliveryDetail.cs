using System;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Domain.Models.Delivery
{
    public class DeliveryDetail
    {
        [Key]
        public Guid Id { get; set; }
        public Guid DeliveryId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Delivery Delivery { get; set; }
    }
}