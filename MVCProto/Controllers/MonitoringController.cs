using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProto.Controllers
{
    public class MonitoringController : Controller
    {
        // GET: Monitoring
        public ActionResult Index()
        {
            Drone drone1 = new Drone();
            Drone drone2 = new Drone();
            Drone drone3 = new Drone();
            Drone[] droneArray = { drone1, drone2, drone3 };

            ViewBag.Drones = droneArray;
            return View();
        }

        
    }
}