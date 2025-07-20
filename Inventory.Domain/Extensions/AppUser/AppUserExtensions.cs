namespace WareSync.Domain;
public static class AppUserExtensions
{
    public static string ToCustomRole(this AppUser user)
    {
        return user.UserRole switch
        {
            UserRole.Admin => CustomRoles.Admin,
            UserRole.WarehouseManager => CustomRoles.WarehouseManager,
            UserRole.WarehouseStaff => CustomRoles.WarehouseStaff,
            UserRole.SalesStaff => CustomRoles.SalesStaff,
            UserRole.DeliveryStaff => CustomRoles.DeliveryStaff,
            UserRole.Accountant => CustomRoles.Accountant,
            UserRole.Auditor => CustomRoles.Auditor,
            _ => throw new NotImplementedException()
        };
    }
}
