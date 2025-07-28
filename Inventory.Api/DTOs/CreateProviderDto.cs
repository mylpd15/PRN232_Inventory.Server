namespace WareSync.Api.DTOs;

public class CreateProviderDto
{
    public string ProviderName { get; set; } = string.Empty;
    public string? ProviderAddress { get; set; }
} 