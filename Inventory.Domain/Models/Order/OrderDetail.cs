using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WareSync.Domain;
public class OrderDetail : AuditableEntity
{
    [Key]
    public int OrderDetailID { get; set; }
    public int OrderQuantity { get; set; }
    public DateTime ExpectedDate { get; set; }
    public DateTime? ActualDate { get; set; }
    // FK
    public int OrderID { get; set; }
    [ForeignKey("OrderID")]
    public Order? Order { get; set; }
    public int ProductID { get; set; }
    [ForeignKey("ProductID")]
    public Product? Product { get; set; }
    public ICollection<Transfer>? Transfers { get; set; }
} 