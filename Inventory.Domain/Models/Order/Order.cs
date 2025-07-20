using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WareSync.Domain;
public class Order : AuditableEntity
{
    [Key]
    public int OrderID { get; set; }
    public DateTime OrderDate { get; set; }
    // FK
    public int ProviderID { get; set; }
    [ForeignKey("ProviderID")]
    public Provider? Provider { get; set; }
    public ICollection<OrderDetail>? OrderDetails { get; set; }
} 