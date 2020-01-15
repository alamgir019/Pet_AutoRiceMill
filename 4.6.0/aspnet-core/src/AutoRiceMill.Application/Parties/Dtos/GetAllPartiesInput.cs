using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRiceMill.Parties.Dtos
{
    public class GetAllPartiesInput: PagedAndSortedResultRequestDto
    {
        public bool IsActive { get; set; }
    }
}
