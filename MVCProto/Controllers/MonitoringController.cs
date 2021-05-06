using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProto.Controllers
{
    public class MonitoringController : Controller
    {
        double[] mass =  { 56.736506, 37.221947 }; 
        // GET: Monitoring
        public ActionResult Index()
        {
            Drone drone1 = new Drone(56.736506, 37.221947);
            Drone drone2 = new Drone(56.736842, 37.218743);
            Drone drone3 = new Drone(56.737078, 37.223097);
            Drone[] droneArray = { drone1, drone2, drone3 };

            ViewBag.Drones = droneArray;
            return View();
        }

        
    }
}