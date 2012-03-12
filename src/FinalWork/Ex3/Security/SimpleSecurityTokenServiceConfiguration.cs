using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Configuration;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.SecurityTokenService;

namespace IdP.Security
{
    public class SimpleSecurityTokenServiceConfiguration : SecurityTokenServiceConfiguration
    {
        public SimpleSecurityTokenServiceConfiguration()
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            var certificates = store.Certificates.OfType<X509Certificate2>().Where(c => c.Subject.Contains("sign.idp.prompt11.local"));
            if (certificates.Count() > 0)
            {
                this.SigningCredentials = new X509SigningCredentials(certificates.First());
            }
            store.Close();
            TokenIssuerName = @"https://idp.prompt11.local:9443";
        }
    }
}