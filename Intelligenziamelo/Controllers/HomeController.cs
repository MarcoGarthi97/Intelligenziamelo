using Intelligenziamelo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public ActionResult SignUp()
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

        public Task<bool> Register(string json)
        {
            var result = JsonConvert.DeserializeObject<JToken>(json);
            DateTime birthday = Convert.ToDateTime(result["Birthday"].ToString());

            User user = new User(result["Username"].ToString(), result["Password"].ToString(), result["Email"].ToString());
            DescriptionUser descriptionUser = new DescriptionUser(result["Username"].ToString(), result["CompleteName"].ToString(), result["Gender"].ToString(),
                result["Phone"].ToString(), result["Country"].ToString(), birthday, false);

            Atlas atlas = new Atlas();
            var insert = atlas.InsertUsers(user, descriptionUser).Result;

            return Task.FromResult<bool>(insert);
        }

        public Task<bool> CheckUsername(string username)
        {
            Atlas atlas = new Atlas();
            var result = atlas.CheckUsername(username);

            return result;
        }

        public Task<bool> CheckEmail(string email)
        {
            Atlas atlas = new Atlas();
            var result = atlas.CheckEmail(email);

            return result;
        }

        public JsonResult GetGenders()
        {
            Atlas atlas = new Atlas();
            var result = atlas.GetGender();

            return Json(result.Result);
        }

        public ActionResult GoToLogin()
        {
            return Json(new { redirectToUrl = Url.Action("Login", "Home") });
        }
        public ActionResult GoToSignUp()
        {
            return Json(new { redirectToUrl = Url.Action("SignUp", "Home") });
        }

        public ActionResult GoToHomePage()
        {
            if(userModel.Login)
                return Json(new { redirectToUrl = Url.Action("HomePage", "Home") });

            return View();
        }

        public Task<bool> LoadDataSet(HttpPostedFileBase file)
        {
            var result = FileSystem.CheckDataSet(file);
            return Task.FromResult<bool>(true);
        }
    }
}