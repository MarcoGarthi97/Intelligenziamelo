using Intelligenziamelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intelligenziamelo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Atlas atlas = new Atlas();
            atlas.AuthenticationLogin("MarcoGarthi", "ciao123");
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}