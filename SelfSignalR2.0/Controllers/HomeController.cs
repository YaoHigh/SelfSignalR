using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfSignalR2._0.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chat(string name)
        {
            Session["userid"] = Guid.NewGuid().ToString().ToUpper();
            Session["username"] = name;
            return View();
        }
    }
}