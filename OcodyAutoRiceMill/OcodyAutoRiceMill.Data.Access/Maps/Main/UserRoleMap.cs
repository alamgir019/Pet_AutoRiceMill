using OcodyAutoRiceMill.Data.Access.Maps.Common;
using OcodyAutoRiceMill.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace OcodyAutoRiceMill.Data.Access.Maps.Main
{
    public class UserRoleMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<UserRole>()
                .ToTable("UserRoles")
                .HasKey(x => x.Id);
        }
    }
}