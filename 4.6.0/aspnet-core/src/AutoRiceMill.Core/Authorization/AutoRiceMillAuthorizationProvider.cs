using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace AutoRiceMill.Authorization
{
    public class AutoRiceMillAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            var party= context.CreatePermission(PermissionNames.Pages_Parties,L("Parties"));
            party.CreateChildPermission(PermissionNames.Pages_Parties_Update,L("UpdateParties"));
            party.CreateChildPermission(PermissionNames.Pages_Parties_Delete,L("DeleteParty"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AutoRiceMillConsts.LocalizationSourceName);
        }
    }
}
