using Microsoft.AspNetCore.Antiforgery;
using AutoRiceMill.Controllers;

namespace AutoRiceMill.Web.Host.Controllers
{
    public class AntiForgeryController : AutoRiceMillControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
