namespace WareSync.Api.DTOs;
public class WarehouseDto
{
    public int WarehouseID { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
    public bool IsRefrigerated { get; set; }
    public int LocationID { get; set; }
    public string? LocationName { get; set; }
} 