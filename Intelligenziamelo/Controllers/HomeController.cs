using Intelligenziamelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Intelligenziamelo.Controllers
{
    public class HomeController : Controller
    {
        public static UserModel userModel;
        public ActionResult Index()
        {
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

        public ActionResult HomePage()
        {
            return View();
        }

        public Task<bool> Logon(string username, string password)
        {
            Atlas atlas = new Atlas();
            var result = atlas.Login(username, password).Result;

            return Task.FromResult<bool>(result);
        }
        public ActionResult GoToHomePage()
        {
            if(userModel.Login)
                return Json(new { redirectToUrl = Url.Action("HomePage", "Home") });

            return View();
        }
    }
}