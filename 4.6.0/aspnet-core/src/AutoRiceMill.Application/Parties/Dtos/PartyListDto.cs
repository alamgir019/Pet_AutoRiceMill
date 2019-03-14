using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRiceMill.Parties.Dtos
{
    [AutoMapFrom(typeof(Party))]
    public class PartyListDto : EntityDto, IHasCreationTime
    {
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Area { get; set; }
        public Nullable<int> districtId { get; set; }
        public Nullable<int> zoneId { get; set; }
        public bool isCashParty { get; set; }
        public bool isActive { get; set; }
        public Nullable<int> ProductId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}