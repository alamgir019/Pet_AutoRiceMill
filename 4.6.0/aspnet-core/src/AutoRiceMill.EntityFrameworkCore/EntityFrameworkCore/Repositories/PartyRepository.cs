using Abp.EntityFrameworkCore;
using AutoRiceMill.Parties;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRiceMill.EntityFrameworkCore.Repositories
{
    public class PartyRepository: AutoRiceMillRepositoryBase<Party, int>, IPartyRepository
    {
        public PartyRepository(IDbContextProvider<AutoRiceMillDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }
}
