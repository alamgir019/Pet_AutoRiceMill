using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using AutoRiceMill.Authorization;
using AutoRiceMill.Parties.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRiceMill.Parties
{
    [AbpAuthorize(PermissionNames.Pages_Parties)]
    public class PartyAppService : AutoRiceMillAppServiceBase, IPartyAppService
    {
        private readonly IRepository<Party> _partyRepository;
        public PartyAppService(IRepository<Party> partyRepository)
        {
            _partyRepository = partyRepository;
        }
        public async Task<ListResultDto<PartyDto>> GetAll(GetAllPartiesInput input)
        {
            var parties = await _partyRepository
                .GetAll()
                .Where(p => p.isActive == input.IsActive)
                .OrderByDescending(p => p.CreationTime)
                .ToListAsync();
            return new ListResultDto<PartyDto>(
                ObjectMapper.Map<List<PartyDto>>(parties)
                );
        }
    }
}