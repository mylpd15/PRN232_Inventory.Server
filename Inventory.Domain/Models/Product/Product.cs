using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WareSync.Domain;
public class Product : AuditableEntity
{
    [Key]
    public int ProductID { get; set; }
    [Required, MaxLength(100)]
    public string ProductCode { get; set; } = string.Empty;
    [MaxLength(100)]
    public string? BarCode { get; set; }
    [MaxLength(2000)]
    public string? ProductName { get; set; }
    [MaxLength(2000)]
    public string? ProductDescription { get; set; }
    [MaxLength(100)]
    public string? ProductCategory { get; set; }
    public int ReorderQuantity { get; set; }
    public decimal PackedWeight { get; set; }
    public decimal PackedHeight { get; set; }
    public decimal PackedWidth { get; set; }
    public decimal PackedDepth { get; set; }
    public bool Refrigerated { get; set; }
    // Navigation
    public ICollection<Inventory>? Inventories { get; set; }
    public ICollection<OrderDetail>? OrderDetails { get; set; }
    public ICollection<DeliveryDetail>? DeliveryDetails { get; set; }
} 