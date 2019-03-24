using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Johan.Repository;
using Johan.DATA;

namespace JohanAgroFood.Controllers
{
    public class LoginController : Controller
    {
        private ILoginUserRepository loginRepository;

        //constructor
        public LoginController()
        {
            if (loginRepository == null)
            {
                this.loginRepository = new DLoginUserRepository(new JohanAgroFoodDBEntities());
            }
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["userId"] = null;
            Session["role"] = null;
            //FormsAuthentication.SignOut();
            //Response.Cookies["userType"].Expires = DateTime.Now.AddDays(-1); // make it expire yesterday
            return RedirectToAction("Index", "Login");
        }
        // POST: Login
        //[Authorize]
        [HttpPost]
        public ActionResult Index(tblUser model, string returnUrl)
        {
            try
            {
                // TODO: Add insert logic here


                var lUser = loginRepository.GetUserById(model);
                if (lUser != null)
                {
                    //FormsAuthentication.SetAuthCookie(model.USERNAME, false);
                    //var role = (SimpleRoleProvider)Roles.Provider;
                    //Roles.AddUserToRole(model.USERNAME, "Admin");
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        Session["userId"] = lUser.userId;
                        Session["role"] = lUser.userType;
                        //FormsAuthentication.SetAuthCookie(lUser.userId,false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "ব্যবহারকারী অথবা পাসওয়ার্ড সঠিক নয়।");
                }
                return View();

            }
            catch (Exception exc)
            {
                ModelState.AddModelError("", exc.Message);
                return View();
            }
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
