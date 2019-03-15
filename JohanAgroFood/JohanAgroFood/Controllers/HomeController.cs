using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JohanAgroFood.Controllers
{
    public class HomeController : Controller
    {
        //[Authorize(Roles = "Admin,Super Admin,User")]     
        public ActionResult Index()
        {

            if (Session["role"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin" || Session["role"].ToString() == "User")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        //[Authorize(Roles = "Admin,Super Admin,User")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            if (Session["role"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (Session["role"].ToString() == "Super Admin" || Session["role"].ToString() == "User")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        //[Authorize(Roles = "Admin,Super Admin,User")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            if (Session["role"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (Session["role"].ToString() == "Admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
