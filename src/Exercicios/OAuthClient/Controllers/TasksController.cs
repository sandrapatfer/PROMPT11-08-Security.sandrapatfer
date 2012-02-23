using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.IO;
using System.Web.Script.Serialization;
using System.Json;

namespace OAuthClient.Controllers
{
    public class TasksController : Controller
    {
        //
        // GET: /Tasks/

        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Request.Params["code"]))
            {
                return new RedirectResult("https://accounts.google.com/o/oauth2/auth?" +
                    "response_type=code&client_id=646897896780.apps.googleusercontent.com&" +
                    "redirect_uri=" + HttpUtility.UrlEncode("http://localhost:49876/Tasks") +
                    "&scope=" + HttpUtility.UrlEncode("https://www.googleapis.com/auth/tasks.readonly"));
            }
            else
            {
                var queryParams = new List<KeyValuePair<string, string>>();
                queryParams.Add(new KeyValuePair<string, string>("code", Request.Params["code"]));
                queryParams.Add(new KeyValuePair<string, string>("redirect_uri", "http://localhost:49876/Tasks"));
                queryParams.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
                queryParams.Add(new KeyValuePair<string, string>("client_id", "646897896780.apps.googleusercontent.com"));
                queryParams.Add(new KeyValuePair<string, string>("client_secret", "1HuwNjxEeNFIMd090yL1uR_X"));
                var form = new FormUrlEncodedContent(queryParams);
                var httpClient = new HttpClient();
                var postResult = httpClient.PostAsync("https://accounts.google.com/o/oauth2/token", form).Result;
                var stream = postResult.Content.ReadAsStreamAsync().Result;
                var value = JsonValue.Load(stream);

                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue((string)value["token_type"], (string)value["access_token"]);
                var stream1 = httpClient.GetAsync("https://www.googleapis.com/tasks/v1/users/sandrapatfer/lists?alt=json&prettyPrint=true").
                    Result.Content.ReadAsStreamAsync().Result;
                var value2 = JsonValue.Load(stream1);
                return Content((string)value2);
            }
        }

        //
        // GET: /Tasks/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Tasks/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Tasks/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Tasks/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Tasks/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Tasks/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Tasks/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
