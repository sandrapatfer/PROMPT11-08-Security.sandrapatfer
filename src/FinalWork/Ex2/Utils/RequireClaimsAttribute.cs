using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ex2.Utils
{
    public class RequireClaimsAttribute : Attribute
    {
        string[] _claims;

        public RequireClaimsAttribute(params string[] claims)
        {
            _claims = claims;
        }

        public string[] Claims
        {
            get { return _claims; }
            set { _claims = value; }
        }
    }
}