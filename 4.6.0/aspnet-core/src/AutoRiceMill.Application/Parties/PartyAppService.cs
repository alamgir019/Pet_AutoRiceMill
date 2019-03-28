using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using AutoRiceMill.Authorization;
using AutoRiceMill.Parties.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using Abp.UI;
using Abp.AutoMapper;

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
                .WhereIf(input.IsActive.HasValue, p => p.isActive == input.IsActive)
                .OrderByDescending(p => p.CreationTime)
                .ToListAsync();                
            return new ListResultDto<PartyDto>(
                ObjectMapper.Map<List<PartyDto>>(parties)
                );
        }
        public async Task Create(CreatePartyInput input)
        {
            Logger.Info("Creating a party for input: " + input);
            var party = ObjectMapper.Map<Party>(input);            
            await _partyRepository.InsertAsync(party);
        }
        [AbpAuthorize(PermissionNames.Pages_Parties_Update)]
        public async Task UpdateParty(UpdatePartyInput input)
        {
            Logger.Info("Updating a party for input: " + input);

            var party = await _partyRepository.FirstOrDefaultAsync(input.Id);
            if (party == null)
            {
                throw new UserFriendlyException(L("CouldNotFindThePartyMessage"));
            }
            ObjectMapper.Map(input, party);
        }
    }
}