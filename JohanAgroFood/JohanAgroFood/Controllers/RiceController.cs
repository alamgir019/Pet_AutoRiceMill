
using Johan.DATA;
using Johan.Repository;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JohanAgroFood.Controllers
{
    public class RiceController : Controller
    {
        private IRiceRepository riceRepository;

        //constructor
        public RiceController()
        {
            if (riceRepository == null)
            {
                this.riceRepository = new DRiceRepository(new JohanAgroFoodDBEntities());
            }
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        public ActionResult RiceInfoIndex()
        {

            //if (Session["role"] == null)
            //{
            //    Session["userId"] = null;
            //    Session["role"] = null;
            //    return RedirectToAction("Index", "Login");
            //}
            //else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            //{
                return View();
            //}
            //else
            //{
            //    Session["userId"] = null;
            //    Session["role"] = null;
            //    return RedirectToAction("Index", "Login");
            //}
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        [HttpPost]
        public JsonResult SaveRiceInfo(tblSell riceInfo)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(riceRepository.SaveRice(riceInfo), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize(Roles = "Admin,Super Admin")]
        [HttpPost]
        public JsonResult EditRiceInfo(tblSell riceInfo)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(riceRepository.EditRiceSell(riceInfo), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize(Roles = "Admin,Super Admin")]
        [HttpPost]
        public JsonResult DeleteRiceInfo(int pk)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(riceRepository.DeleteRiceSell(pk), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize(Roles = "Admin,Super Admin")]
        public JsonResult GetRiceInfo()
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                var sellList = riceRepository.GetRiceInfo();
                return Json(sellList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize(Roles = "Admin,Super Admin")]
        public JsonResult LoadRice()
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                List<tblProduct> rices = riceRepository.LoadRice();
                return Json(rices, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        //[Authorize(Roles = "Admin,Super Admin,User")]
        public ActionResult rptRiceInfo()
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return RedirectToAction("Index", "Login");
            }
            else if (Session["role"].ToString() == "User" || Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
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

        //[HttpPost]
        //[Authorize(Roles = "Admin,Super Admin,User")]
        public ActionResult Preview(string partyId, string from, string to)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/Reports"), "riceInfo.rdlc");
                if (!System.IO.File.Exists(path) || string.IsNullOrEmpty(partyId) || string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
                {
                    return View("rptRiceInfo");
                }
                int pId = Convert.ToInt32(partyId);
                tblSell riceRpt = new tblSell();
                riceRpt.partyId = pId;
                riceRpt.fromDate = Convert.ToDateTime(from);
                riceRpt.toDate = Convert.ToDateTime(to);
                List<object> objLst = riceRepository.GetRiceInfoRpt(riceRpt);
                var reportViewModel = riceRepository.GetRepoertViewModel(objLst, pId, from, to);
                reportViewModel.FileName = path;
                var renderedBytes = reportViewModel.RenderReport();
                if (reportViewModel.ViewAsAttachment)
                    Response.AddHeader("content-disposition", reportViewModel.ReporExportFileName);
                return File(renderedBytes, reportViewModel.LastmimeType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[Authorize(Roles = "Admin,Super Admin,User")]
        public ActionResult PreviewCom(string partyId, string from, string to)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/Reports"), "riceInfoCom.rdlc");
                if (!System.IO.File.Exists(path) || string.IsNullOrEmpty(partyId) || string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
                {
                    return View("rptRiceInfo");
                }
                int pId = Convert.ToInt32(partyId);
                tblSell riceRpt = new tblSell();
                riceRpt.partyId = pId;
                riceRpt.fromDate = Convert.ToDateTime(from);
                riceRpt.toDate = Convert.ToDateTime(to);
                List<object> objLst = riceRepository.GetRiceInfoRpt(riceRpt);
                var reportViewModel = riceRepository.GetRepoertViewModel(objLst, pId, from, to);
                reportViewModel.FileName = path;
                var renderedBytes = reportViewModel.RenderReport();
                if (reportViewModel.ViewAsAttachment)
                    Response.AddHeader("content-disposition", reportViewModel.ReporExportFileName);
                return File(renderedBytes, reportViewModel.LastmimeType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}