using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Johan.DATA;
using Johan.Repository;
using Johan.Repository.IDAL;

namespace JohanAgroFood.Controllers
{
    public class BalanceTransferPaddyController : Controller
    {
        private IBalancePaddyRepository balancePaddyRepository;
        //constructor
        public BalanceTransferPaddyController()
        {
            if (balancePaddyRepository == null)
            {
                this.balancePaddyRepository = new DBalancePaddyRepository(new JohanAgroFoodDBEntities());
            }
        }

        // GET: /LoanBalance/
        //[Authorize(Roles = "Admin,Super Admin")]
        public ActionResult Index()
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return RedirectToAction("Index", "Login");
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return View();
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return RedirectToAction("Index", "Login");
            }
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        public ActionResult BalancePaddyIndex()
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return RedirectToAction("Index", "Login");
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return View();
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return RedirectToAction("Index", "Login");
            }
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        public JsonResult Save(tblDue objDue)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(balancePaddyRepository.SaveBalancePaddy(objDue), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        //[Authorize(Roles = "Admin,Super Admin,User")]
        public JsonResult GetBalancePaddyInfo()
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(balancePaddyRepository.GetBalancePaddyInfo(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}