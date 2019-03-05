using System.Threading.Tasks;
using AutoRiceMill.Configuration.Dto;

namespace AutoRiceMill.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
