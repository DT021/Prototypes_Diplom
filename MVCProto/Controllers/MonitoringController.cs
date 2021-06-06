using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace MVCProto.Controllers
{
    public class MonitoringController : Controller
    {
        Drone drone1 = new Drone(1, 56.736506, 37.221947);
        Drone drone2 = new Drone(2, 56.736842, 37.218743);
        Drone drone3 = new Drone(3, 56.737078, 37.223097);
        Drone drone4 = new Drone(4, 56.732837, 37.221782);

        /// <summary>
        /// /////////////////////////////
        /// </summary>
        static string[] groups = new string[] { "ID1;ID4", "ID2;ID3" };


        public ActionResult Index()
        {

            Random r = new Random();
            int idx = (int)(r.NextDouble() + 0.5); 
            ViewBag.Drones = groups[idx];

            //Drone[] droneArray = { drone1, drone2, drone3, drone4 };
            //ViewBag.Drones = droneArray;

            return View();
        }

        public ActionResult Index2()
        {
            Drone[] droneArray = { drone1, drone2, drone3, drone4 };
            ViewBag.Drones = droneArray;
            return View("Index");
        }

        public ActionResult Ticker()
        {

            Random r = new Random();
            //int idx = (int)(r.NextDouble() + 0.5);
            //ViewBag.Drones = groups[idx];
            ViewBag.Drones = groups[0];
            return View();
        }


        public ActionResult Ticker2()
        {

            Random r = new Random();
            //int idx = (int)(r.NextDouble() + 0.5);
            //ViewBag.Drones = groups[idx];
            ViewBag.Drones = groups[1];
            return View();
        }



    }
}