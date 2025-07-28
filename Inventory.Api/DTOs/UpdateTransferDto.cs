namespace WareSync.Api.DTOs;

public class UpdateTransferDto
{
    public int TransferID { get; set; }
    public int TransferQuantity { get; set; }
    public DateTime SentDate { get; set; }
    public DateTime? ReceivedDate { get; set; }
    public int OrderDetailID { get; set; }
    public int WarehouseID { get; set; }
} 