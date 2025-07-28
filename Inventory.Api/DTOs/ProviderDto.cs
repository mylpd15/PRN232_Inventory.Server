using System.ComponentModel.DataAnnotations;

namespace WareSync.Api.DTOs;
public class ProviderDto
{
    [Key]
    public int ProviderID { get; set; }
    public string ProviderName { get; set; } = string.Empty;
    public string? ProviderAddress { get; set; }
} 