using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WareSync.Domain;
public class DeliveryDetail : AuditableEntity
{
    [Key]
    public int DeliveryDetailID { get; set; }
    public int DeliveryQuantity { get; set; }
    public DateTime ExpectedDate { get; set; }
    public DateTime? ActualDate { get; set; }
    // FK
    public int DeliveryID { get; set; }
    [ForeignKey("DeliveryID")]
    public Delivery? Delivery { get; set; }
    public int ProductID { get; set; }
    [ForeignKey("ProductID")]
    public Product? Product { get; set; }
} 