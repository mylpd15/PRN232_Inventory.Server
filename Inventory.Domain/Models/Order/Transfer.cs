using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WareSync.Domain;
public class Transfer : AuditableEntity
{
    [Key]
    public int TransferID { get; set; }
    public int TransferQuantity { get; set; }
    public DateTime SentDate { get; set; }
    public DateTime? ReceivedDate { get; set; }
    // FK
    public int OrderDetailID { get; set; }
    [ForeignKey("OrderDetailID")]
    public OrderDetail? OrderDetail { get; set; }
    public int WarehouseID { get; set; }
    [ForeignKey("WarehouseID")]
    public Warehouse? Warehouse { get; set; }
} 