using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Johan.DATA;
using Johan.Repository.DAL;
using Johan.Repository.IDAL;
using Microsoft.Ajax.Utilities;

namespace JohanAgroFood.Controllers
{
    public class ReportController : Controller
    {
        private IReportRepository reportRepository;
        //
        // GET: /Report/
        public ReportController()
        {
            if (reportRepository == null)
            {
                this.reportRepository = new DReportRepository(new JohanAgroFoodDBEntities());
            }
        }
        //[Authorize(Roles = "Admin,Super Admin,User")]
        public ActionResult Index()
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
        public ActionResult RptDailySellInfo()
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
        public ActionResult paddyDailyBuy()
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
        public ActionResult rptOtherConsump()
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
        // [Authorize(Roles = "Admin,Super Admin,User")]
        public ActionResult rptIncome()
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
        public ActionResult rptHollarConsump()
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
        public ActionResult rptPaddyStock()
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
        // [Authorize(Roles = "Admin,Super Admin,User")]
        public ActionResult Preview(string parentId, string todayDate)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/Reports"), "dailySellInfo.rdlc");
                if (!System.IO.File.Exists(path) || string.IsNullOrEmpty(parentId) || string.IsNullOrEmpty(todayDate))
                {
                    return View("rptDailySellInfo");
                }
                int pId = Convert.ToInt32(parentId);
                tblSell dailySellRpt = new tblSell();
                dailySellRpt.parentProd = pId;

                dailySellRpt.toDate = Convert.ToDateTime(todayDate);
                List<object> objLst = reportRepository.GetDailySellInfoRpt(dailySellRpt);
                var reportViewModel = reportRepository.GetRepoertViewModel(objLst, pId, todayDate);
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
        public ActionResult PreviewPaddyBuy(string todayDate)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/Reports"), "dailyPaddyBuy.rdlc");
                if (!System.IO.File.Exists(path) || string.IsNullOrEmpty(todayDate))
                {
                    return View("paddyDailyBuy");
                }
                tblBuy dailyBuyRpt = new tblBuy();

                dailyBuyRpt.toDate = Convert.ToDateTime(todayDate);
                List<object> objLst = reportRepository.GetDailyBuyInfoRpt(dailyBuyRpt);
                var reportViewModel = reportRepository.GetDailyBuyVM(objLst, todayDate);
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
        public ActionResult OtherConsump(string startDate, string endDate, int sectorId)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/Reports"), "otherConsumption.rdlc");
                if (!System.IO.File.Exists(path) || string.IsNullOrEmpty(endDate) || string.IsNullOrEmpty(startDate))
                {
                    return View("rptOtherConsump");
                }
                List<object> objLst = reportRepository.GetOtherConsumpRpt(startDate, endDate, sectorId);
                var reportViewModel = reportRepository.GetOtherConsumpViewModel(objLst, startDate, endDate, sectorId);
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
        public ActionResult previewIncome(string startDate, string endDate, int sectorId)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/Reports"), "income.rdlc");
                if (!System.IO.File.Exists(path) || string.IsNullOrEmpty(endDate) || string.IsNullOrEmpty(startDate))
                {
                    return View("rptIncome");
                }
                List<object> objLst = reportRepository.GetIncomeRpt(startDate, endDate, sectorId);
                var reportViewModel = reportRepository.GetIncomeVM(objLst, startDate, endDate, sectorId);
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
        public ActionResult HollarConsump(string startDate, string endDate, string stockId)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/Reports"), "hollarConsumption.rdlc");
                if (!System.IO.File.Exists(path) || string.IsNullOrEmpty(endDate) || string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(stockId))
                {
                    return View("rptHollarConsump");
                }
                List<object> objLst = reportRepository.GetHollarConsumpRpt(startDate, endDate, stockId);
                var reportViewModel = reportRepository.GetHollarConsumpViewModel(objLst, startDate, endDate, stockId);
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
        // [Authorize(Roles = "Admin,Super Admin,User")]
        public ActionResult PaddyStock(string stockId, string productId, string from, string to)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/Reports"), "paddyStockInfo.rdlc");
                if (!System.IO.File.Exists(path) || string.IsNullOrEmpty(stockId) || string.IsNullOrEmpty(productId) || string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
                {
                    return View("rptStockInfo");
                }
                int pId = Convert.ToInt32(productId);
                int stkId = Convert.ToInt32(stockId);
                PaddyTransaction stockRpt = new PaddyTransaction();
                stockRpt.prodId = pId;
                stockRpt.stockId = stkId;
                stockRpt.fromDate = Convert.ToDateTime(from);
                stockRpt.toDate = Convert.ToDateTime(to);
                List<object> objLst = reportRepository.GetPaddyStockRpt(stockRpt);
                var reportViewModel = reportRepository.GetPaddyStockViewModel(objLst, stkId, pId, from, to);
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