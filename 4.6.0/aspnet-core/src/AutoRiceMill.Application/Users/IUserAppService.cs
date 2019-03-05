using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AutoRiceMill.Roles.Dto;
using AutoRiceMill.Users.Dto;

namespace AutoRiceMill.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
