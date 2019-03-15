using Johan.DATA;
using Johan.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JohanAgroFood.Controllers
{
    public class WithdrawController : Controller
    {
        IWithdrawRepository withdrawRepo = null;
         //constructor
        public WithdrawController()
        {
            if (withdrawRepo == null)
            {
                this.withdrawRepo = new DWithdrawRepository(new JohanAgroFoodDBEntities());
            }
        }
        // GET: /WidthdrawMoney/
        public ActionResult Withdraw()
        {
            return View();
        }
        public JsonResult Save(tblCostingSource objCosting)
        {
            return Json(withdrawRepo.Save(objCosting), JsonRequestBehavior.AllowGet);
        }
	}
}