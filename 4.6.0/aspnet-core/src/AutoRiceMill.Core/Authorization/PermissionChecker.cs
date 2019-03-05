using Abp.Authorization;
using AutoRiceMill.Authorization.Roles;
using AutoRiceMill.Authorization.Users;

namespace AutoRiceMill.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
