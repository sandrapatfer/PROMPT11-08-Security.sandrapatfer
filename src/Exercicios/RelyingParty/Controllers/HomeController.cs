using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Claims;

namespace RelyingParty.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public string Index()
        {
            var ident = this.User as IClaimsPrincipal;
            if (ident == null)
                return "no identity";
            var emailClaim = ident.Identities[0].Claims.Where(c => c.ClaimType == ClaimTypes.Email).FirstOrDefault();
            var roleClaim = ident.Identities[0].Claims.Where(c => c.ClaimType == ClaimTypes.Role).FirstOrDefault();
            var myClaim = ident.Identities[0].Claims.Where(c => c.ClaimType == "myclaim").FirstOrDefault();
            return "Hi there " + (emailClaim != null ? emailClaim.Value : "stranger")
                + ", your role is " + (roleClaim != null ? roleClaim.Value : "empty")
                + " and the new is " + (myClaim != null? myClaim.Value : "not working");
        }

        public ActionResult IndexView()
        {
            return View();
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Home/Create

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
        // GET: /Home/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Home/Edit/5

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
        // GET: /Home/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Home/Delete/5

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
