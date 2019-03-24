using Johan.DATA;
using Johan.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JohanAgroFood.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository userRepository;

        //constructor
        public UserController()
        {
            if (userRepository == null)
            {
                this.userRepository = new DUserRepository(new JohanAgroFoodDBEntities());
            }
        }
        // GET: Login
        //[Authorize(Roles = "Super Admin")]
        public ActionResult Index()
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return RedirectToAction("Index", "Login");
            }
            else if (Session["role"].ToString() == "Super Admin")
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

        // [Authorize(Roles = "Super Admin")]
        public JsonResult GetUser()
        {
            var userList = userRepository.GetUser();
            return Json(userList, JsonRequestBehavior.AllowGet);
        }

        //[Authorize(Roles = "Super Admin")]
        [HttpPost]
        public JsonResult SaveUser(tblUser user)
        {
            return Json(userRepository.SaveUser(user), JsonRequestBehavior.AllowGet);
        }
        //[Authorize(Roles = "Super Admin")]
        public JsonResult Edit(tblUser objUser)
        {
            return Json(userRepository.Edit(objUser), JsonRequestBehavior.AllowGet);
        }
        //[Authorize(Roles = "Super Admin")]
        public JsonResult Delete(int pk)
        {
            return Json(userRepository.Delete(pk), JsonRequestBehavior.AllowGet);
        }
    }
}