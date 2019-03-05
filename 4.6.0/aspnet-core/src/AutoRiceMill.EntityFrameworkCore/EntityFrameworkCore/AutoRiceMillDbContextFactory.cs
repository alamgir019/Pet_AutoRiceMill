using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using AutoRiceMill.Configuration;
using AutoRiceMill.Web;

namespace AutoRiceMill.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class AutoRiceMillDbContextFactory : IDesignTimeDbContextFactory<AutoRiceMillDbContext>
    {
        public AutoRiceMillDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AutoRiceMillDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            AutoRiceMillDbContextConfigurer.Configure(builder, configuration.GetConnectionString(AutoRiceMillConsts.ConnectionStringName));

            return new AutoRiceMillDbContext(builder.Options);
        }
    }
}
