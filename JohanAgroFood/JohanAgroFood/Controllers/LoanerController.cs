using Johan.DATA;
using Johan.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JohanAgroFood.Controllers
{
    public class LoanerController : Controller
    {
        private ILoanerRepository loanerRepository;

        //constructor
        public LoanerController()
        {
            if (loanerRepository == null)
            {
                this.loanerRepository = new DLoanerRepository(new JohanAgroFoodDBEntities());
            }
        }
        // GET: Loaner
        //[Authorize(Roles = "Admin,Super Admin")]
        public ActionResult LoanerIndex()
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
        [HttpPost]
        public JsonResult SaveLoaner(tblLoanar loaner)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                loaner.createDate = DateTime.ParseExact(loaner.createDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture);

                return Json(loanerRepository.SaveLoaner(loaner), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                loaner.createDate = DateTime.ParseExact(loaner.createDate.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture);

                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        public JsonResult GetLoaner()
        {
            var loanerList = loanerRepository.GetLoaner();
            return Json(loanerList, JsonRequestBehavior.AllowGet);
        }
    }
}