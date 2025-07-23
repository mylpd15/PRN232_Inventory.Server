using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WareSync.Domain.Enums;

namespace WareSync.Domain;
public class Delivery : AuditableEntity
{
    [Key]
    public int DeliveryID { get; set; }
    public DateTime SalesDate { get; set; }
    // FK
    public int CustomerID { get; set; }
    [ForeignKey("CustomerID")]
    public Customer? Customer { get; set; }
    public ICollection<DeliveryDetail>? DeliveryDetails { get; set; }
    public DeliveryStatus Status { get; set; } = DeliveryStatus.Pending;
} 