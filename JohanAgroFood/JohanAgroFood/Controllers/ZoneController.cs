using Johan.DATA;
using Johan.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JohanAgroFood.Controllers
{
    public class ZoneController : Controller
    {
        private IZoneRepository zoneRepository;

        //constructor
        public ZoneController()
        {
            if (zoneRepository == null)
            {
                this.zoneRepository = new DZoneRepository(new JohanAgroFoodDBEntities());
            }
        }
        // GET: Login
        // [Authorize(Roles = "Admin,Super Admin")]
        public JsonResult GetZone()
        {
            var zoneList = zoneRepository.GetZone();
            return Json(zoneList, JsonRequestBehavior.AllowGet);
        }

    }
}