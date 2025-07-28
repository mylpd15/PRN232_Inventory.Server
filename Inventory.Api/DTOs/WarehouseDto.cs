using System.ComponentModel.DataAnnotations;

namespace WareSync.Api.DTOs;
public class WarehouseDto
{
    [Key]
    public int WarehouseID { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
    public bool IsRefrigerated { get; set; }
    public int LocationID { get; set; }
} 