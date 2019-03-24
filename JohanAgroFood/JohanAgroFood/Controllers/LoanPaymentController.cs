using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Johan.DATA;
using Johan.Repository.DAL;
using Johan.Repository.IDAL;
using System.Globalization;

namespace JohanAgroFood.Controllers
{
    public class LoanPaymentController : Controller
    {
        private ILoanPaymentRepository loanPaymentRepository;
        //Constructor
        public LoanPaymentController()
        {
            if (loanPaymentRepository == null)
            {
                loanPaymentRepository = new DLoanPaymentRepository(new JohanAgroFoodDBEntities());
            }
        }

        // GET: /loanPayment/
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
        public ActionResult LoanPayment()
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
        //[Authorize(Roles = "Admin,Super Admin,User")]
        public JsonResult GetLoanPayment(int objPartyId)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(loanPaymentRepository.GetLoanPayment(objPartyId), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        public JsonResult Save(tblCostingSource objLoanCostingSource)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                objLoanCostingSource.date = DateTime.ParseExact(objLoanCostingSource.date.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture);

                return Json(0, JsonRequestBehavior.AllowGet);

            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                objLoanCostingSource.date = DateTime.ParseExact(objLoanCostingSource.date.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture);

                return Json(loanPaymentRepository.Save(objLoanCostingSource), JsonRequestBehavior.AllowGet);

            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(0, JsonRequestBehavior.AllowGet);

            }
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        public JsonResult GetLoanPaid(string objDate)
        {
            DateTime obDate = Convert.ToDateTime(objDate);
            return Json(loanPaymentRepository.GetLoanPaid(obDate), JsonRequestBehavior.AllowGet);
        }
    }
}