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
    public class CarController : Controller
    {
        private ICarRepository carRepository;

        //constructor
        public CarController()
        {
            if (carRepository == null)
            {
                 this.carRepository = new DCarRepository(new JohanAgroFoodDBEntities());
            }
        }





        //[Authorize(Roles = "Admin,Super Admin")]
        public ActionResult CarIndex()
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
        public JsonResult SaveCar(tblCar objcar)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(carRepository.SaveCar(objcar), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult EditCar(tblCar objCar)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(carRepository.EditCar(objCar), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult DeleteCar(int pk)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(carRepository.DeleteCar(pk), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetCar()
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                var carList = carRepository.GetCar();
                return Json(carList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult rptCar()
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
        public ActionResult GeneralReport(string from, string to)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/Reports"), "car.rdlc");
                if (!System.IO.File.Exists(path) || string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
                {
                    return View("rptCar");
                }
                tblCar carRpt = new tblCar();
                carRpt.fromDate = Convert.ToDateTime(from);
                carRpt.toDate = Convert.ToDateTime(to);
                List<object> objLst = carRepository.GetCarRpt(carRpt);
                var reportViewModel = carRepository.CarViewModel(objLst, from, to);
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