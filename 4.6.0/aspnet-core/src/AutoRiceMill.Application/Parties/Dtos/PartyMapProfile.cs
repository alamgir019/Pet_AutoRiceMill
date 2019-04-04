using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRiceMill.Parties.Dtos
{
    public class PartyMapProfile:Profile
    {
        public PartyMapProfile()
        {
            CreateMap<Party, PartyDetailOutput>();
        }
    }
}
