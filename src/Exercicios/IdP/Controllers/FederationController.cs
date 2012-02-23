using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Protocols.WSFederation;
using Microsoft.IdentityModel.Web;
using IdP.Security;

namespace IdP.Controllers
{
    public class FederationController : Controller
    {
        //
        // GET: /Federation/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Federation/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Federation/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Federation/Create

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
        // GET: /Federation/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Federation/Edit/5

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
        // GET: /Federation/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Federation/Delete/5

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

        [HttpPost, Authorize]
        public void Issue()
        {
            var req = WSFederationMessage.CreateFromUri(Request.Url);
            var resp = FederatedPassiveSecurityTokenServiceOperations.ProcessSignInRequest(
                req as SignInRequestMessage,
                User,
                new SimpleSecurityTokenService(new SimpleSecurityTokenServiceConfiguration()));
            resp.Write(Response.Output);
            Response.Flush();
            Response.End();
        }
    }
}
