using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestFlight.Configuration;

namespace Testflight.Web.Controllers
{
    public class HomeController : Controller
    {
        private IMongoSession session;

        public HomeController(IMongoSession session)
        {
            this.session = session;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
