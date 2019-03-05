using Abp.Application.Services.Dto;

namespace AutoRiceMill.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

