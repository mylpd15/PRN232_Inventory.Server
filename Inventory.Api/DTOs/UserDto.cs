namespace WareSync.Api.DTOs;
public class UserDto
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string? Username { get; set; }
    public string? Email { get; set; }
    public bool IsDisabled { get; set; }
    public string UserRole { get; set; } = string.Empty;
} 