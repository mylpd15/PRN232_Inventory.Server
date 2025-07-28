using System.ComponentModel.DataAnnotations;

namespace WareSync.Api.DTOs;
public class OrderDetailDto
{
    [Key]
    public int OrderDetailID { get; set; }
    public int OrderID { get; set; }
    public int ProductID { get; set; }
    public string? ProductName { get; set; }
    public int OrderQuantity { get; set; }
    public DateTime ExpectedDate { get; set; }
    public DateTime? ActualDate { get; set; }
} 