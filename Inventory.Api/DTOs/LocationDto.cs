namespace WareSync.Api.DTOs;
public class LocationDto
{
    public int LocationID { get; set; }
    public string LocationName { get; set; } = string.Empty;
    public string? LocationAddress { get; set; }
} 