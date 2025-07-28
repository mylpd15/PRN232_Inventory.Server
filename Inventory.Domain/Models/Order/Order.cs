using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WareSync.Domain.Enums;

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

    // Thêm status và lý do reject
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public string? RejectReason { get; set; }

    // New: Warehouse reference
    public int WarehouseID { get; set; }
    public Warehouse Warehouse { get; set; } = null!;  // Navigation property
    public ICollection<OrderDetail>? OrderDetails { get; set; }
} 