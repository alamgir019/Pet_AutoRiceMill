using OcodyAutoRiceMill.Data.Model;

namespace OcodyAutoRiceMill.Security
{
    public interface ISecurityContext
    {
        User User { get; }

        bool IsAdministrator { get; }
    }
}