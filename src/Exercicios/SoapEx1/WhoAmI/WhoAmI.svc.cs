using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Microsoft.IdentityModel.Claims;

namespace SoapEx1
{
    public class WhoAmI : IWhoAmI
    {
        public string Get()
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                StringBuilder str = new StringBuilder();
                var claimsIdentity = Thread.CurrentPrincipal.Identity as IClaimsIdentity;
                foreach (Claim claim in claimsIdentity.Claims)
                {
                    str.AppendLine(string.Format("{0} = {1}", claim.ClaimType, claim.Value));
                }
                return str.ToString();
                //return string.Format("hello {0}", Thread.CurrentPrincipal.Identity.Name);
            }
            else
            {
                return "not authenticated";
            }
        }

    }
}
