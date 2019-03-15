using OcodyAutoRiceMill.Data.Access.Maps.Common;
using OcodyAutoRiceMill.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace OcodyAutoRiceMill.Data.Access.Maps.Main
{
    public class RoleMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Role>()
                .ToTable("Roles")
                .HasKey(x => x.Id);
        }
    }
}