using System.ComponentModel.DataAnnotations;

namespace WareSync.Api.DTOs;
public class InventoryDto
{
    [Key]
    public int InventoryID { get; set; }
    public int QuantityAvailable { get; set; }
    public int MinimumStockLevel { get; set; }
    public int MaximumStockLevel { get; set; }
    public int ReorderPoint { get; set; }
    public int ProductID { get; set; }
    public string? ProductName { get; set; }
    public int WarehouseID { get; set; }
    public string? WarehouseName { get; set; }
} 