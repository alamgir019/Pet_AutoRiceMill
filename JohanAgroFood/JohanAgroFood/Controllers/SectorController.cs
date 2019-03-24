using Johan.DATA;
using Johan.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JohanAgroFood.Controllers
{
    public class SectorController : Controller
    {
        private ISectorRepository sectorRepository;
        //////////////   sector and common element same
        //constructor
        public SectorController()
        {
            if (sectorRepository == null)
            {
                this.sectorRepository = new DSectorRepository(new JohanAgroFoodDBEntities());
            }
        }
        // GET: Sector
        // [Authorize(Roles = "Admin,Super Admin")]
        public ActionResult SectorIndex()
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
        public ActionResult zone()
        {

            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return RedirectToAction("Index", "Login");
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin" || Session["role"].ToString() == "User")
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
        public ActionResult district()
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
        public JsonResult DeleteSector([System.Web.Http.FromBody] tblCommonElement sector)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(sectorRepository.DeleteSector(sector), JsonRequestBehavior.AllowGet);
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
        public JsonResult EditSector([System.Web.Http.FromBody] tblCommonElement sector)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(sectorRepository.EditSector(sector), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        //         [Authorize(Roles = "Admin,Super Admin")]
        [HttpPost]
        public JsonResult SaveSector(tblCommonElement sector)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin" || Session["role"].ToString() == "User")
            {
                return Json(sectorRepository.SaveSector(sector), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        public JsonResult GetSector(int elemCode)
        {
            var SectorList = sectorRepository.GetSector(elemCode);
            return Json(SectorList, JsonRequestBehavior.AllowGet);
        }
    }
}