using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPartialAsync.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            System.Threading.Thread.Sleep(2000);
            Models.UserModel model = new Models.UserModel();
            model.Users = new List<Models.User>();
            for (int i = 1; i < 6; i++)
            {
                model.Users.Add(new Models.User() { UserId = i, UserName = "test-" + i });
            }
            return View(model);
        }

        public ActionResult Sections()
        {

            return View();
        }
    }
}