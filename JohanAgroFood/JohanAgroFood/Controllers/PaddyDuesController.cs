using Johan.DATA;
using Johan.Repository.DAL;
using Johan.Repository.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JohanAgroFood.Controllers
{
    public class PaddyDuesController : Controller
    {
        // GET: PaddyDues
        //[Authorize(Roles = "Admin,Super Admin")]
        public ActionResult PaddyDues()
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
        public ActionResult ReturnBag()
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
        private IPaddyDuesRepository duePaymentRepository;
        //constructor

        public PaddyDuesController()
        {
            if (duePaymentRepository == null)
            {
                this.duePaymentRepository = new DPaddyDuesRepository(new JohanAgroFoodDBEntities());
            }
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        public JsonResult GetRemainingBag(int partyId)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(duePaymentRepository.GetRemainingBag(partyId), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        //[Authorize(Roles = "Admin,Super Admin,User")]
        [HttpPost]
        public JsonResult SaveBag(BagTransaction objBag)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(duePaymentRepository.SaveBag(objBag), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize(Roles = "Admin,Super Admin")]
        public JsonResult Save(tblIncomeSource objDueIncomeSource)
        {
            //return Json(duePaymentRepository.Save(objDueIncomeSource), JsonRequestBehavior.AllowGet);
            return null;
        }
        [Authorize(Roles = "Admin,Super Admin")]
        public JsonResult GetDuePayment(int objPartyId)
        {
            //return Json(duePaymentRepository.GetDuePayment(objPartyId), JsonRequestBehavior.AllowGet);
            return null;
        }
    }
}