using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AutoRiceMill.Configuration;
using AutoRiceMill.EntityFrameworkCore;
using AutoRiceMill.Migrator.DependencyInjection;

namespace AutoRiceMill.Migrator
{
    [DependsOn(typeof(AutoRiceMillEntityFrameworkModule))]
    public class AutoRiceMillMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public AutoRiceMillMigratorModule(AutoRiceMillEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(AutoRiceMillMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                AutoRiceMillConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AutoRiceMillMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
