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
    public class IncomeController : Controller
    {
        private IIncomeRepository incomeRepository;

        //constructor
        public IncomeController()
        {
            if (incomeRepository == null)
            {
                this.incomeRepository = new DIncomeRepository(new JohanAgroFoodDBEntities());
            }
        }
        // GET: Income
        //[Authorize(Roles = "Admin,Super Admin,User")]
        public ActionResult IncomeIndex()
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

        //[Authorize(Roles = "Super Admin")]
        [HttpPost]
        public JsonResult DeleteIncome([System.Web.Http.FromBody] tblIncomeSource income)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(incomeRepository.DeleteIncome(income), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize(Roles = "Super Admin")]
        [HttpPost]
        public JsonResult EditIncome([System.Web.Http.FromBody] tblIncomeSource income)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                income.date = DateTime.ParseExact(income.date.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                return Json(incomeRepository.EditIncome(income), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize(Roles = "Admin,Super Admin")]
        [HttpPost]
        public JsonResult SaveIncome(tblIncomeSource income)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                income.date = DateTime.ParseExact(income.date.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                return Json(incomeRepository.SaveIncome(income), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);

            }
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        public JsonResult GetIncome()
        {
            var IncomeList = incomeRepository.GetIncome();
            return Json(IncomeList, JsonRequestBehavior.AllowGet);
        }
    }
}