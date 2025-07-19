using System.ComponentModel.DataAnnotations;

namespace WareSync.Domain;
public class GoogleSignInVM
{
    [Required]
    public string IdToken { get; set; }
    public UserRole UserRole { get; set; } = UserRole.WarehouseStaff;
}