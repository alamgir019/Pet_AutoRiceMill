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
    public class PaddyTransferController : Controller
    {
        private IPaddyTransferRepository paddyTransferRepo;

        //constructor
        public PaddyTransferController()
        {
            if (paddyTransferRepo == null)
            {
                this.paddyTransferRepo = new DPaddyTransferRepository(new JohanAgroFoodDBEntities());
            }
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        public ActionResult PaddyTransfer()
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
        public JsonResult GetSackWeights(int productId, int stockId)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(paddyTransferRepo.GetSackWeights(productId, stockId), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        public JsonResult Save(STK_Balance objStkBalance)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(paddyTransferRepo.SavePaddyTransfer(objStkBalance), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize(Roles = "Admin,Super Admin")]
        public JsonResult GetPaddyTransferInfos()
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(paddyTransferRepo.GetPaddyTransferInfos(), JsonRequestBehavior.AllowGet);
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