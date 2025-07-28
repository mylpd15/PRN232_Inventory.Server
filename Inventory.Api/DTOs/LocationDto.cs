using System.ComponentModel.DataAnnotations;

namespace WareSync.Api.DTOs;
public class LocationDto
{
    [Key]
    public int LocationID { get; set; }
    public string LocationName { get; set; } = string.Empty;
    public string? LocationAddress { get; set; }
} 