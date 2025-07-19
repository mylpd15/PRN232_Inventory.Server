using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WareSync.Domain;
public class Warehouse : AuditableEntity
{
    [Key]
    public int WarehouseID { get; set; }
    [Required, MaxLength(100)]
    public string WarehouseName { get; set; } = string.Empty;
    public bool IsRefrigerated { get; set; }
    // FK
    public int LocationID { get; set; }
    [ForeignKey("LocationID")]
    public Location? Location { get; set; }
    public ICollection<Inventory>? Inventories { get; set; }
    public ICollection<Transfer>? Transfers { get; set; }
} 