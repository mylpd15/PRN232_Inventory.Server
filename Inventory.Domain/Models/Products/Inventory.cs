using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WareSync.Domain;
public class Inventory : AuditableEntity
{
    [Key]
    public int InventoryID { get; set; }
    public int QuantityAvailable { get; set; }
    public int MinimumStockLevel { get; set; }
    public int MaximumStockLevel { get; set; }
    public int ReorderPoint { get; set; }
    // FK
    public int ProductID { get; set; }
    [ForeignKey("ProductID")]
    public Product? Product { get; set; }
    public int WarehouseID { get; set; }
    [ForeignKey("WarehouseID")]
    public Warehouse? Warehouse { get; set; }
    public ICollection<InventoryLog>? InventoryLogs { get; set; }

}