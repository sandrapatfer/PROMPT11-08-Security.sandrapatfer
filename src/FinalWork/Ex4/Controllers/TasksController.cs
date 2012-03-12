using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Json;
using Ex4.Models;
using System.Web.Script.Serialization;
using Ex4.Utils;

namespace Ex4.Controllers
{
    public class TasksController : Controller
    {
        //
        // GET: /Tasks/

        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Request.Params["code"]))
            {
                return GoogleProcessor.Redirect();
            }

            var googleProc = new GoogleProcessor(Request.Params["code"]);
            return View(googleProc.GetFirstListTasks());
        }

    }
}
