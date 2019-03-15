using Johan.DATA;
using Johan.Repository;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JohanAgroFood.Controllers
{
    public class ParticleSellController : Controller
    {
        private IParticleSellRepository sellRepository;

        //constructor
        public ParticleSellController()
        {
            if (sellRepository == null)
            {
                this.sellRepository = new DParticleSellRepository(new JohanAgroFoodDBEntities());
            }
        }
        // GET: Loaner
        //[Authorize(Roles = "Admin,Super Admin")]
        public ActionResult particleInfoIndex()
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
        public ActionResult rptParticleInfo()
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
        public ActionResult rptParticleGeneral()
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

        //[Authorize(Roles = "Admin,Super Admin")]
        [HttpPost]
        public JsonResult SaveParticleInfo(tblSell riceInfo)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(sellRepository.SaveParticle(riceInfo), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        public JsonResult GetParticleInfo()
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                var sellList = sellRepository.GetParticleInfo();
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
                var stkList = sellRepository.GetStock();
                return Json(stkList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        // stock portion

        //[Authorize(Roles = "Admin,Super Admin")]
        public ActionResult ParticleStock()
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
        public JsonResult SaveParticleStock(STK_Balance particleStk)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(sellRepository.SaveParticleStock(particleStk), JsonRequestBehavior.AllowGet);
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
        public JsonResult EditParticleInfo(tblSell objParticleInfo)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(sellRepository.EditParticleSell(objParticleInfo), JsonRequestBehavior.AllowGet);
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
        public JsonResult DeleteParticleInfo(int pk)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(sellRepository.DeleteParticleSell(pk), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        //[Authorize(Roles = "Admin,Super Admin,User")]
        public ActionResult GeneralReport(string from, string to)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/Reports"), "generalParticle.rdlc");
                if (!System.IO.File.Exists(path) || string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
                {
                    return View("rptParticleGeneral");
                }
                tblSell particleRpt = new tblSell();
                particleRpt.fromDate = Convert.ToDateTime(from);
                particleRpt.toDate = Convert.ToDateTime(to);
                List<object> objLst = sellRepository.GetParticleGeneralRpt(particleRpt);
                var reportViewModel = sellRepository.ParticleGeneralViewModel(objLst, from, to);
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
        //[HttpPost]
        public ActionResult Preview(string partyId, string from, string to)
        {
            try
            {
                LocalReport lr = new LocalReport();
                string path = Path.Combine(Server.MapPath("~/Reports"), "particleInfo.rdlc");
                if (!System.IO.File.Exists(path) || string.IsNullOrEmpty(partyId) || string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
                {
                    return View("rptRiceInfo");
                }
                else
                {
                    lr.ReportPath = path;
                }
                //tblSell prtlRpt = new tblSell();
                //prtlRpt.partyId = Convert.ToInt32(partyId);
                //prtlRpt.fromDate = Convert.ToDateTime(from);
                //prtlRpt.toDate = Convert.ToDateTime(to);
                //List<object> particleInfLst = sellRepository.GetParticleInfoRpt(prtlRpt);
                //ReportDataSource reportDataSource = new ReportDataSource();
                //reportDataSource.Name = "dataset_GetParticleInfo";

                //reportDataSource.Value = particleInfLst;
                //lr.DataSources.Add(reportDataSource);
                string reportType = "PDF";
                //string reportType1 = "Image";
                string mimeType;
                string LastmimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo =

                "<DeviceInfo>" +
                "  <OutputFormat>" + reportType + "</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>0.25in</MarginLeft>" +
                "  <MarginRight>0.25in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;
                renderedBytes = lr.Render(
                    reportType,
                    deviceInfo,
                    out LastmimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);
                return File(renderedBytes, LastmimeType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        //[Authorize(Roles = "Super Admin")]
        [HttpPost]
        public JsonResult DeleteParticleStock([System.Web.Http.FromBody] STK_Balance particleStk)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(sellRepository.DeleteParticleStock(particleStk), JsonRequestBehavior.AllowGet);
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
        public JsonResult EditParticleStock([System.Web.Http.FromBody] STK_Balance particleStk)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(sellRepository.EditParticleStock(particleStk), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize(Roles = "Admin,Super Admin,User")]
        public JsonResult GetParticleStock()
        {
            var riceStkLst = sellRepository.GetParticleStock();
            return Json(riceStkLst, JsonRequestBehavior.AllowGet);
        }

    }
}