namespace WareSync.Api.DTOs;
public class CustomerDto
{
    public int CustomerID { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string? CustomerAddress { get; set; }
} 