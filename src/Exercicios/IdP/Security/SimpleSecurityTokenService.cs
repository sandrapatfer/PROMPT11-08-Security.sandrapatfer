using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using Microsoft.IdentityModel.SecurityTokenService;
using Microsoft.IdentityModel.Configuration;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Protocols.WSTrust;

namespace IdP.Security
{
    public class SimpleSecurityTokenService : SecurityTokenService
    {
        private const string addressExpected = "http://localhost:6020/ClaimsAwareWebService";

        public SimpleSecurityTokenService(SecurityTokenServiceConfiguration configuration)
            : base( configuration )
        {
        }

        protected override Microsoft.IdentityModel.Claims.IClaimsIdentity GetOutputClaimsIdentity(Microsoft.IdentityModel.Claims.IClaimsPrincipal principal, Microsoft.IdentityModel.Protocols.WSTrust.RequestSecurityToken request, Scope scope)
        {
            if (null == principal)
            {
                throw new InvalidRequestException("The caller's principal is null.");
            }

            // Get the incoming IClaimsIdentity from IPrincipal 
            IClaimsIdentity callerIdentity = (IClaimsIdentity)principal.Identity;

            // Create the output IClaimsIdentity
            IClaimsIdentity outputIdentity = new ClaimsIdentity();

            // Create a name claim from the incoming identity.
            Claim nameClaim = new Claim(ClaimTypes.Name, callerIdentity.Name);

            // Create an 'Age' claim with a value of 25. In a real scenario, this may likely be looked up from a database.
            Claim ageClaim = new Claim("http://WindowsIdentityFoundationSamples/2008/05/AgeClaim", "25", ClaimValueTypes.Integer);

            // Add the name
            outputIdentity.Claims.Add(nameClaim);
            outputIdentity.Claims.Add(ageClaim);

            return outputIdentity;
        }

        protected override Scope GetScope(Microsoft.IdentityModel.Claims.IClaimsPrincipal principal, Microsoft.IdentityModel.Protocols.WSTrust.RequestSecurityToken request)
        {
            // Validate the AppliesTo on the incoming request
            ValidateAppliesTo(request.AppliesTo);

            // Normally the STS will have a trust relationship with the RP and can look up a trusted encrypting certficate 
            // using the AppliesTo endpoint. This is necessary to ensure that only the RP will be able to read the claims.
            //
            // In this sample the certificate of the AppliesTo Identity is used to encrypt the contents, so there is no
            // validation of any trust relationship with the RP. Since the certificate is not validated, 
            // a malicious client can provide a known certificate allowing it to read the returned claims.
            // For this reason, THIS APPROACH SHOULD NOT BE USED if the claims should be kept private. It may be reasonable,
            // though, if the STS is simply verifying public information such as the client's email address.

            // Get RP certificate
            //X509CertificateEndpointIdentity appliesToIdentity = (X509CertificateEndpointIdentity)request.AppliesTo.Identity;

            //X509EncryptingCredentials encryptingCredentials = new X509EncryptingCredentials(appliesToIdentity.Certificates[0]);
            
            // Create the scope using the request AppliesTo address and the STS signing certificate
            Scope scope = new Scope(request.AppliesTo.Uri.AbsoluteUri, 
                SecurityTokenServiceConfiguration.SigningCredentials);
            return scope;
        }

        void ValidateAppliesTo(EndpointAddress appliesTo)
        {
            if (appliesTo == null)
            {
                throw new InvalidRequestException("The appliesTo is null.");
            }

            if (!appliesTo.Uri.Equals(new Uri(addressExpected)))
            {
                Console.WriteLine("The relying party address is not valid. ");
                throw new InvalidRequestException(String.Format("The relying party address is not valid. Expected value is {0}, the actual value is {1}.", addressExpected, appliesTo.Uri.AbsoluteUri));
            }
        }
    }
}