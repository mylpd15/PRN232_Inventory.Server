namespace WareSync.Api.DTOs;
public class TransferDto
{
    public int TransferID { get; set; }
    public int TransferQuantity { get; set; }
    public DateTime SentDate { get; set; }
    public DateTime? ReceivedDate { get; set; }
    public int OrderDetailID { get; set; }
    public string? ProductName { get; set; }
    public int WarehouseID { get; set; }
    public string? WarehouseName { get; set; }
} 