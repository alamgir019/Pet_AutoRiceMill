using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Johan.DATA;
using Johan.Repository;
using System.Globalization;

namespace JohanAgroFood.Controllers
{
    public class BalanceTransferController : Controller
    {

        private IBalanceRepository balanceRepository;
        //constructor
        public BalanceTransferController()
        {
            if (balanceRepository == null)
            {
                this.balanceRepository = new DBalanceRepository(new JohanAgroFoodDBEntities());
            }
        }

        // GET: /LoanBalance/
        //[Authorize(Roles = "Admin,Super Admin")]
        public ActionResult BalanceIndex()
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
        public JsonResult Save(tblPayable objPayable)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                if (objPayable.date != null)
                {
                    objPayable.date = DateTime.ParseExact(objPayable.date.Value.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                return Json(balanceRepository.SaveBalance(objPayable), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        public JsonResult GetBalanceInfo()
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(balanceRepository.GetBalanceInfo(), JsonRequestBehavior.AllowGet);
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