using MVCProto.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MVCProto.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult DroneControl()
        {
            return View();         
        }

        
        public ActionResult UpdateId(int id)
        {
            ViewBag.UpdatingId = id;
           

            return View("~/Views/Home/Index.cshtml");

            //return View("~/Views/Monitoring/Index.cshtml");
        }
    }
}