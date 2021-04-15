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
            return View();
        }

        public ActionResult NewIndex()
        {
            return View();
        }


        public ActionResult Monitoring()
        {
            return View();
        }
    }
}