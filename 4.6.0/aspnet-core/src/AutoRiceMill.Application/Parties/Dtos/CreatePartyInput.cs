using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AutoRiceMill.Parties.Dtos
{
    [AutoMapTo(typeof(Party))]
    public class CreatePartyInput
    {
        //public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Area { get; set; }
        public bool IsActive { get; set; }
    }
}
