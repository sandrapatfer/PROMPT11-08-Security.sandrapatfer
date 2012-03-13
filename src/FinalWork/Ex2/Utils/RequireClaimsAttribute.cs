using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Claims;

namespace Ex2.Utils
{
    [AttributeUsageAttribute(AttributeTargets.Method)]
    public class RequireClaimsAttribute : ActionFilterAttribute
    {
        public string[] Claims { get; set; }

        public RequireClaimsAttribute(params string[] claims)
        {
            Claims = claims;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var ident = HttpContext.Current.User as IClaimsPrincipal;
            if (ident != null && ident.Identity.IsAuthenticated == true)
            {
                foreach (var claim in Claims)
                {
                    if (ident.Identities[0].Claims.Where(c => c.ClaimType == claim).Count() == 0)
                    {
                        var view = new ViewResult() { ViewName = "Error" };
                        view.ViewData.Model = string.Format("Claim {0} is missing", claim);
                        filterContext.Result = view;
                        return;
                    }
                }
            }

            // method deals with not authorized and full claims
        }


    }
}