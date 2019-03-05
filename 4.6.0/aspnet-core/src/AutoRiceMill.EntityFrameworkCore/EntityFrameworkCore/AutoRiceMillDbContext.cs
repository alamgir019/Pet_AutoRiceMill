using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using AutoRiceMill.Authorization.Roles;
using AutoRiceMill.Authorization.Users;
using AutoRiceMill.MultiTenancy;

namespace AutoRiceMill.EntityFrameworkCore
{
    public class AutoRiceMillDbContext : AbpZeroDbContext<Tenant, Role, User, AutoRiceMillDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public AutoRiceMillDbContext(DbContextOptions<AutoRiceMillDbContext> options)
            : base(options)
        {
        }
    }
}
