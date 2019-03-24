using System.IO;
using Johan.DATA;
using Johan.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JohanAgroFood.Controllers
{
    public class HuskSellController : Controller
    {
        private IHuskSellRepository huskSellRepository;

        //constructor
        public HuskSellController()
        {
            if (huskSellRepository == null)
            {
                this.huskSellRepository = new DHuskSellRepository(new JohanAgroFoodDBEntities());
            }
        }
        // GET: Loaner
        //[Authorize(Roles = "Admin,Super Admin")]
        public ActionResult HuskInfoIndex()
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
        public ActionResult rptHuskInfo()
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
        public ActionResult rptHuskIncWithd()
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
        //[Authorize(Roles = "Admin,Super Admin,User")]
        [HttpPost]
        public JsonResult SaveHuskInfo(tblSell riceInfo)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(huskSellRepository.SaveHusk(riceInfo), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        public JsonResult GetHuskInfo()
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                var sellList = huskSellRepository.GetHuskInfo();
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
        public JsonResult LoadHusk(int stockId)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                List<tblProduct> husks = huskSellRepository.LoadHusk(stockId);
                return Json(husks, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        public JsonResult GetStock()
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                var stkList = huskSellRepository.GetStock();
                return Json(stkList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        //[Authorize(Roles = "Super Admin")]
        [HttpPost]
        public JsonResult DeleteHuskInfo(int pk)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(huskSellRepository.DeleteHuskSell(pk), JsonRequestBehavior.AllowGet);
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
        public JsonResult EditHuskInfo(tblSell huskInfo)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(huskSellRepository.EditHuskInfo(huskInfo), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        //[Authorize(Roles = "Admin,Super Admin,User")]
        public ActionResult Preview(string partyId, string from, string to)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/Reports"), "huskInfo.rdlc");
                if (!System.IO.File.Exists(path) || string.IsNullOrEmpty(partyId) || string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
                {
                    return View("rptHuskInfo");
                }
                int pId = Convert.ToInt32(partyId);
                tblSell huskRpt = new tblSell();
                huskRpt.partyId = pId;
                huskRpt.fromDate = Convert.ToDateTime(from);
                huskRpt.toDate = Convert.ToDateTime(to);
                List<object> objLst = huskSellRepository.GetHuskInfoRpt(huskRpt);
                var reportViewModel = huskSellRepository.GetRepoertViewModel(objLst, pId, from, to);
                reportViewModel.FileName = path;
                var renderedBytes = reportViewModel.RenderReport();
                if (reportViewModel.ViewAsAttachment)
                {
                    Response.AddHeader("content-disposition", reportViewModel.ReporExportFileName);

                }
                return File(renderedBytes, reportViewModel.LastmimeType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //[Authorize(Roles = "Admin,Super Admin,User")]
        public ActionResult Income(string from, string to)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/Reports"), "huskIncWithd.rdlc");
                if (!System.IO.File.Exists(path) || string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
                {
                    return View("rptHuskIncome");
                }
                tblSell huskRpt = new tblSell();
                huskRpt.fromDate = Convert.ToDateTime(from);
                huskRpt.toDate = Convert.ToDateTime(to);
                List<object> objLst = huskSellRepository.GetHuskIncomeRpt(huskRpt);
                var reportViewModel = huskSellRepository.GetIncomeViewModel(objLst, from, to);
                reportViewModel.FileName = path;
                var renderedBytes = reportViewModel.RenderReport();
                if (reportViewModel.ViewAsAttachment)
                {
                    Response.AddHeader("content-disposition", reportViewModel.ReporExportFileName);
                }
                return File(renderedBytes, reportViewModel.LastmimeType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}