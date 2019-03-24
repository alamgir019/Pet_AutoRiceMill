using Johan.DATA;
using Johan.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JohanAgroFood.Controllers
{
    public class CommonDataController : Controller
    {
        public JohanAgroFoodDBEntities context = new JohanAgroFoodDBEntities();
        public CommonDataController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }
        public JsonResult GetAllProduct()
        {
            var prods = CommonData.GetAllProduct(context);
            return Json(prods, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductByName(string productName)
        {
            var prods = CommonData.GetProductByName(context, productName);
            return Json(prods, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllParty()
        {
            var partyList = CommonData.GetAllParty(context);
            return Json(partyList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllStock()
        {
            var stocks = CommonData.GetAllStock(context);
            return Json(stocks, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllSector()
        {
            var sectors = CommonData.GetAllSector(context);
            return Json(sectors, JsonRequestBehavior.AllowGet);
        }

    }
}