namespace WareSync.Api.DTOs;

public class UpdateProviderDto
{
    public int ProviderID { get; set; }
    public string ProviderName { get; set; } = string.Empty;
    public string? ProviderAddress { get; set; }
} 