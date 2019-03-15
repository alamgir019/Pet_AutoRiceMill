using System.IO;
using Johan.DATA;
using Johan.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace JohanAgroFood.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository empRepository;

        //constructor
        public EmployeeController()
        {
            if (empRepository == null)
            {
                this.empRepository = new DEmployeeRepository(new JohanAgroFoodDBEntities());
            }
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        public ActionResult employee()
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
        public ActionResult salary()
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
        public JsonResult SaveEmp(Employee employee)
        {
            //SqlConnection con;
            //SqlCommand cmd;
            //con = new SqlConnection("Data Source=208.91.198.196;Initial Catalog=agrojepq_JohanAgroFoodDB;User ID=johan;Password=johan@#$%234;Integrated Security=False;MultipleActiveResultSets=True;Application Name=EntityFramework");
            //con.Open();
            //string s = null;
            //s = DateTime.Now.Date.ToString("d-MM-yyyy") + "_agrojepq_JohanAgroFoodDB";
            ////query("Backup database agrojepq_JohanAgroFoodDB" + ComboBoxDatabaseName.Text + " to disk='" + s + "'");
            ////string pt = "D:/test.bak";
            //var pt = Server.MapPath("~/dbbak");
            //cmd = new SqlCommand(string.Format(@"BACKUP DATABASE agrojepq_JohanAgroFoodDB TO  DISK = N'{0}' WITH  INIT ,  NOUNLOAD ,  NOSKIP ,  STATS = 10,  NOFORMAT", pt), con);
            //cmd.ExecuteNonQuery();
            //con.Close();
            //return null;
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        public JsonResult GetEmpSal(SalaryPayment salary)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(empRepository.GetEmpSal(salary), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        //[Authorize(Roles = "Admin,Super Admin")]
        public JsonResult GetAllEmployee()
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(empRepository.GetAllEmployee(), JsonRequestBehavior.AllowGet);
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
        public JsonResult EditEmp(Employee employee)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(empRepository.EditEmp(employee), JsonRequestBehavior.AllowGet);
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
        public JsonResult EditSalary(SalaryPayment salary)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(empRepository.EditSalary(salary), JsonRequestBehavior.AllowGet);
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
        public JsonResult SaveSalary(SalaryPayment salary)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(null, JsonRequestBehavior.AllowGet);
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