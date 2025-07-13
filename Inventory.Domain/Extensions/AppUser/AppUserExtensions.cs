namespace Inventory.Domain;
public static class AppUserExtensions
{
    public static string ToCustomRole(this AppUser user)
    {
        return user.UserRole switch
        {
            UserRole.Admin => CustomRoles.Admin,
            UserRole.Coordinator => CustomRoles.Coordinator,
            UserRole.WarehouseManager => CustomRoles.WarehouseManager,
            UserRole.WarehouseStaff => CustomRoles.WarehouseStaff,
            _ => throw new NotImplementedException()
        };
    }
}
