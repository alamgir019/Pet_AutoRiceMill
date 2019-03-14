using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRiceMill.Parties
{
    [Table("Parties")]
    public class Party : Entity, IHasCreationTime
    {
        [Required]
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Area { get; set; }
        public Nullable<int> districtId { get; set; }
        public Nullable<int> zoneId { get; set; }
        public bool isCashParty { get; set; }
        public bool isActive { get; set; }
        public Nullable<int> ProductId { get; set; }
        public DateTime CreationTime { get; set; }

        public Party()
        {
            CreationTime = Clock.Now;
        }
    }
}
