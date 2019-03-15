using OcodyAutoRiceMill.Data.Access.Maps.Common;
using OcodyAutoRiceMill.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace OcodyAutoRiceMill.Data.Access.Maps.Main
{
    public class UserMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<User>()
                .ToTable("Users")
                .HasKey(x => x.Id);
        }
    }
}