using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRiceMill.Parties.Dtos
{
    [AutoMapTo(typeof(Party))]
    public class UpdatePartyInput : CreatePartyInput, IEntityDto
    {
        public int Id { get; set; }
    }
}
