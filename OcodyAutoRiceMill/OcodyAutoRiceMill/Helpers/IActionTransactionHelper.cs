using Microsoft.AspNetCore.Mvc.Filters;

namespace OcodyAutoRiceMill.Helpers
{
    public interface IActionTransactionHelper
    {
        void BeginTransaction();
        void EndTransaction(ActionExecutedContext filterContext);
        void CloseSession();
    }
}