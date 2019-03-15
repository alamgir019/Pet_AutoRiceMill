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
    public class StockController : Controller
    {
        private IStockRepository stkRepository;

        //constructor
        public StockController()
        {
            if (stkRepository == null)
            {
                this.stkRepository = new DStockRepository(new JohanAgroFoodDBEntities());
            }
        }
        // GET: Login
        //[Authorize(Roles = "Admin,Super Admin")]
        public ActionResult StockIndex()
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
        public ActionResult ProductStock()
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
        public JsonResult GetProdStocks()
        {
            var riceStkLst = stkRepository.GetProdStocks();
            return Json(riceStkLst, JsonRequestBehavior.AllowGet);
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        [HttpPost]
        public JsonResult SaveProdStock(STK_Balance prodStk)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(stkRepository.SaveProdStock(prodStk), JsonRequestBehavior.AllowGet);
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
        public JsonResult DeleteProdStock([System.Web.Http.FromBody] STK_Balance prodStk)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(stkRepository.DeleteProdStock(prodStk), JsonRequestBehavior.AllowGet);
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
        public JsonResult EditProdStock([System.Web.Http.FromBody] STK_Balance prodStk)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(stkRepository.EditProdStock(prodStk), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize(Roles = "Admin,Super Admin,User")]
        public ActionResult rptStockInfo()
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
        //[Authorize(Roles = "Admin,Super Admin,User")]
        public ActionResult Preview(string stockId, string productId, string from, string to)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/Reports"), "stockInfo.rdlc");
                if (!System.IO.File.Exists(path) || string.IsNullOrEmpty(stockId) || string.IsNullOrEmpty(productId) || string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
                {
                    return View("rptStockInfo");
                }
                int sId = Convert.ToInt32(stockId);
                int pId = Convert.ToInt32(productId);
                STK_Transaction stockRpt = new STK_Transaction();
                stockRpt.stockId = sId;
                stockRpt.prodId = pId;
                stockRpt.fromDate = Convert.ToDateTime(from);
                stockRpt.toDate = Convert.ToDateTime(to);
                List<object> objLst = stkRepository.GetStockInfoRpt(stockRpt);
                var reportViewModel = stkRepository.GetRepoertViewModel(objLst, sId, pId, from, to);
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
        public JsonResult GetStock(STK_tblStock stk)
        {
            var stkList = stkRepository.GetStock(stk);
            return Json(stkList, JsonRequestBehavior.AllowGet);
        }

        // [Authorize(Roles = "Admin,Super Admin")]
        [HttpPost]
        public JsonResult SaveStock(STK_tblStock stk)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(stkRepository.SaveStock(stk), JsonRequestBehavior.AllowGet);
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
        public JsonResult EditStock([System.Web.Http.FromBody] STK_tblStock stk)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(stkRepository.EditStock(stk), JsonRequestBehavior.AllowGet);
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
        public JsonResult DeleteStock(int pk)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(stkRepository.DeleteStock(pk), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}