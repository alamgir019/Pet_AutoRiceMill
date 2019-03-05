using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace AutoRiceMill.EntityFrameworkCore
{
    public static class AutoRiceMillDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<AutoRiceMillDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<AutoRiceMillDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
