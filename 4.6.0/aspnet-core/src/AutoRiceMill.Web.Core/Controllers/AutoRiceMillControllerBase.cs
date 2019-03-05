using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace AutoRiceMill.Controllers
{
    public abstract class AutoRiceMillControllerBase: AbpController
    {
        protected AutoRiceMillControllerBase()
        {
            LocalizationSourceName = AutoRiceMillConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
