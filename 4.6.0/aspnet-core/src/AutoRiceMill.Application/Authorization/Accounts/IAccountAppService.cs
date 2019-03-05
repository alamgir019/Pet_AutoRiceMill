using System.Threading.Tasks;
using Abp.Application.Services;
using AutoRiceMill.Authorization.Accounts.Dto;

namespace AutoRiceMill.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
