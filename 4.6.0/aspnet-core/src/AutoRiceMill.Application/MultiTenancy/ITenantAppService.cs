using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AutoRiceMill.MultiTenancy.Dto;

namespace AutoRiceMill.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

