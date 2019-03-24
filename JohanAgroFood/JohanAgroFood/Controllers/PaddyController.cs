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
    public class PaddyController : Controller
    {     
        private IPaddyRepository paddyRepository;

        //constructor
        public PaddyController()
        {
            if (paddyRepository == null)
            {
                this.paddyRepository = new DPaddyRepository(new JohanAgroFoodDBEntities());
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PaddyInfoIndex()
        {
            return View();
        }

        public ActionResult rptPaddyInfo()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SavePaddy(tblBuy paddyInfo)
        {
            return Json(paddyRepository.SavePaddy(paddyInfo), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPaddyInfo()
        {
            var jsones = Json(paddyRepository.GetPaddyInfo(), JsonRequestBehavior.AllowGet);
            jsones.MaxJsonLength = int.MaxValue;
            return jsones;
        }
        [HttpPost]
        public JsonResult EditPaddyInfo(tblBuy paddyInfo)
        {
            var value = Request.Cookies["userType"] == null ? null : Request.Cookies["userType"].Value;
            if (value != null && value.ToUpper() == "Super Admin".ToUpper())
            {
                return Json(paddyRepository.EditPaddyInfo(paddyInfo), JsonRequestBehavior.AllowGet);
            }
            else
                return null;
        }

        public JsonResult DeletePaddy(int pk)
        {
            return Json(paddyRepository.DeletePaddy(pk), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Preview(string partyId, string from, string to)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/Reports"), "paddyInfo.rdlc");
                if (!System.IO.File.Exists(path) || string.IsNullOrEmpty(partyId) || string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
                {
                    return View("rptPaddyInfo");
                }
                int pId = Convert.ToInt32(partyId);
                tblBuy paddyRpt = new tblBuy();
                paddyRpt.partyId = pId;
                paddyRpt.fromDate = Convert.ToDateTime(from);
                paddyRpt.toDate = Convert.ToDateTime(to);
                List<object> objLst = paddyRepository.GetPaddyInfoRpt(paddyRpt);
                var reportViewModel = paddyRepository.GetReportViewModel(objLst, pId, from, to);
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
        public ActionResult PreviewCom(string partyId, string from, string to)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/Reports"), "paddyInfoCom.rdlc");
                if (!System.IO.File.Exists(path) || string.IsNullOrEmpty(partyId) || string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
                {
                    return View("rptPaddyInfo");
                }
                int pId = Convert.ToInt32(partyId);
                tblBuy paddyRpt = new tblBuy();
                paddyRpt.partyId = pId;
                paddyRpt.fromDate = Convert.ToDateTime(from);
                paddyRpt.toDate = Convert.ToDateTime(to);
                List<object> objLst = paddyRepository.GetPaddyInfoRpt(paddyRpt);
                var reportViewModel = paddyRepository.GetReportViewModel(objLst, pId, from, to);
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