using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Web.Mvc;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Web;

namespace Ex2.Utils
{
    public class Authentication
    {
        public static string Verify(IPrincipal user, string url)
        {
            var ident = user as IClaimsPrincipal;
            if (ident == null || ident.Identity.IsAuthenticated == false)
            {
                var signin = FederatedAuthentication.WSFederationAuthenticationModule.CreateSignInRequest("1",
                    url, false);
                return signin.WriteQueryString();
            }
            return null;
        }

        internal static string VerifyClaims(object[] callingMethodClaimsAttributes, IPrincipal user)
        {
            var ident = user as IClaimsPrincipal;
            if (ident != null && ident.Identity.IsAuthenticated == true)
            {
                foreach (RequireClaimsAttribute attr in callingMethodClaimsAttributes)
                {
                    foreach (var claim in attr.Claims)
                    {
                        if (ident.Identities[0].Claims.Where(c => c.ClaimType == claim) == null)
                        {
                            return string.Format("Claim {0} is missing", claim);
                        }
                    }
                }
                return null;
            }
            else
            {
                return "Not authenticated";
            }
        }
    }
}