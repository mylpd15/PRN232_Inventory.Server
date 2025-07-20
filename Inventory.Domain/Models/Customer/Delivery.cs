using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
} 