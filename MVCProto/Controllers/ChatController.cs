using MVCProto.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using System.Threading;

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
            
            

            return View("~/Views/Home/Index.cshtml");

        }
    }
}