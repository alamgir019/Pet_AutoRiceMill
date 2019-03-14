using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using AutoRiceMill.Authorization.Roles;
using AutoRiceMill.Authorization.Users;
using AutoRiceMill.MultiTenancy;
using AutoRiceMill.Parties;

namespace AutoRiceMill.EntityFrameworkCore
{
    public class AutoRiceMillDbContext : AbpZeroDbContext<Tenant, Role, User, AutoRiceMillDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Party> Parties { get; set; }
        public AutoRiceMillDbContext(DbContextOptions<AutoRiceMillDbContext> options)
            : base(options)
        {
        }
    }
}
