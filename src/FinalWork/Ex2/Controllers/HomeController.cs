using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Web;
using Ex2.Utils;

namespace Ex2.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Intro()
        {
            return View();
        }

        // GET: /Home/Index
        [RequireClaims(ClaimTypes.Name, ClaimTypes.Email)]
        public ActionResult Index()
        {
            var redirect = Authentication.Verify(this.User, Request.Url.AbsoluteUri);
            if (redirect != null)
            {
                return Redirect(redirect);
            }

            var verificationError = Authentication.VerifyClaims(
                this.GetType().GetMethod("Index").GetCustomAttributes(typeof(RequireClaimsAttribute), true),
                this.User);
            if (verificationError == null)
            {
                return View();
            }
            else
            {
                return View("Error", verificationError);
            }
        }
    }
}
