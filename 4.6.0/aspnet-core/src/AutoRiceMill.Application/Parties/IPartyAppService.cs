using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AutoRiceMill.Parties.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoRiceMill.Parties
{
    public interface IPartyAppService: IApplicationService
    {
        Task<ListResultDto<PartyListDto>> GetAll(GetAllPartiesInput input);
    }
}
