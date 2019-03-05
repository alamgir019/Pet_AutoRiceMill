using System.Threading.Tasks;
using Abp.Application.Services;
using AutoRiceMill.Sessions.Dto;

namespace AutoRiceMill.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
