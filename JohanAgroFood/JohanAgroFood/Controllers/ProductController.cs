using Johan.DATA;
using Johan.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JohanAgroFood.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository ProductRepository;

        //constructor
        public ProductController()
        {
            if (ProductRepository == null)
            {
                this.ProductRepository = new DProductRepository(new JohanAgroFoodDBEntities());
            }
        }
        // GET: Login
        // [Authorize(Roles = "Admin,Super Admin")]
        public ActionResult ProductIndex()
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
        public JsonResult GetProduct(tblProduct prod)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                var productList = ProductRepository.GetProduct(prod);
                return Json(productList, JsonRequestBehavior.AllowGet);
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
        public JsonResult SaveProduct(tblProduct product)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "Super Admin")
            {
                return Json(ProductRepository.SaveProduct(product), JsonRequestBehavior.AllowGet);
                //return View();
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
        public JsonResult EditProduct([System.Web.Http.FromBody] tblProduct product)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(ProductRepository.EditProduct(product), JsonRequestBehavior.AllowGet);
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
        public JsonResult DeleteProduct(int pk)
        {
            if (Session["role"] == null)
            {
                Session["userId"] = null;
                Session["role"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else if (Session["role"].ToString() == "Super Admin")
            {
                return Json(ProductRepository.DeleteProduct(pk), JsonRequestBehavior.AllowGet);
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