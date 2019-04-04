using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRiceMill.Parties.Dtos
{
    [AutoMapFrom(typeof(Party))]
    public class PartyDetailOutput : FullAuditedEntityDto<int>
    {
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Area { get; set; }
        public bool isActive { get; set; }
    }
}
