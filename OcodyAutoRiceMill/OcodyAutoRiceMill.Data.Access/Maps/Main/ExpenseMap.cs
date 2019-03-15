using OcodyAutoRiceMill.Data.Access.Maps.Common;
using OcodyAutoRiceMill.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace OcodyAutoRiceMill.Data.Access.Maps.Main
{
    public class ExpenseMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Expense>()
                .ToTable("OcodyAutoRiceMill")
                .HasKey(x => x.Id);
        }
    }
}