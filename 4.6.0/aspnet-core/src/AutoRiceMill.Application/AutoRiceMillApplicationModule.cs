using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AutoRiceMill.Authorization;

namespace AutoRiceMill
{
    [DependsOn(
        typeof(AutoRiceMillCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class AutoRiceMillApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<AutoRiceMillAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(AutoRiceMillApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
