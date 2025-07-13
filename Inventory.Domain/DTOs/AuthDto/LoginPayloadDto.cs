namespace Inventory.Domain;
public class LoginPayloadDto
{
    public TokenPayloadDto AccessToken { get; set; }
    public AppUser AppUser { get; set; }
}
