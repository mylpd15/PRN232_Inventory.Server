using System.ComponentModel.DataAnnotations;

namespace WareSync.Api;
public class ProductDto
{
    [Key]
    public int ProductID { get; set; }
    public string ProductCode { get; set; } = string.Empty;
    public string? BarCode { get; set; }
    public string? ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public string? ProductCategory { get; set; }
    public int ReorderQuantity { get; set; }
    public decimal PackedWeight { get; set; }
    public decimal PackedHeight { get; set; }
    public decimal PackedWidth { get; set; }
    public decimal PackedDepth { get; set; }
    public bool Refrigerated { get; set; }
    public List<ProductPriceDto> Prices { get; set; } = new();
} 