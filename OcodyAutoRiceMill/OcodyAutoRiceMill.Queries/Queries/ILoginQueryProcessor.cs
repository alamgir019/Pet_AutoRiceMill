using System.Threading.Tasks;
using OcodyAutoRiceMill.Api.Models.Login;
using OcodyAutoRiceMill.Api.Models.Users;
using OcodyAutoRiceMill.Data.Model;
using OcodyAutoRiceMill.Queries.Models;

namespace OcodyAutoRiceMill.Queries.Queries
{
    public interface ILoginQueryProcessor
    {
        UserWithToken Authenticate(string username, string password);
        Task<User> Register(RegisterModel model);
        Task ChangePassword(ChangeUserPasswordModel requestModel);
    }
}