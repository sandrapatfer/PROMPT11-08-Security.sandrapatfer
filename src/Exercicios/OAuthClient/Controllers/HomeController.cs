using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAuthClient.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/TasksImplicitExample
        [HttpGet]
        public ActionResult TasksImplicitExample()
        {
            return View();
        }

        // GET: /Home/TasksImplicitExampleProcessor
        [HttpGet]
        public ActionResult TasksImplicitExampleProcessor()
        {
            return View();
        }
    }
}
