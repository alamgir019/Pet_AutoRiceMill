using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Johan.DATA;
using Johan.Repository;
using Johan.Repository.DAL;
using Johan.Repository.IDAL;

namespace JohanAgroFood.Controllers
{
    public class DuePaymentController : Controller
    {
        private IDuePaymentRepository duePaymentRepository;
        //constructor

        public DuePaymentController()
        {
            if (duePaymentRepository == null)
            {
                this.duePaymentRepository = new DDuePaymentRepository(new JohanAgroFoodDBEntities());
            }
        }
        // GET: /DuePayment/
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
        public JsonResult Save(tblIncomeSource objDueIncomeSource)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(duePaymentRepository.Save(objDueIncomeSource), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize(Roles = "Admin,Super Admin")]
        public JsonResult GetDuePayment(int objPartyId)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(duePaymentRepository.GetDuePayment(objPartyId), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize(Roles = "Admin,Super Admin")]
        public JsonResult GetDuePaid(string objDate)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);

            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                DateTime obDate = Convert.ToDateTime(objDate);
                return Json(duePaymentRepository.GetDuePaid(obDate), JsonRequestBehavior.AllowGet);
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