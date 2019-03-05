using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AutoRiceMill.Configuration;

namespace AutoRiceMill.Web.Host.Startup
{
    [DependsOn(
       typeof(AutoRiceMillWebCoreModule))]
    public class AutoRiceMillWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public AutoRiceMillWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AutoRiceMillWebHostModule).GetAssembly());
        }
    }
}
