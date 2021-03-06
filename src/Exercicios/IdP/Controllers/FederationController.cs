﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Protocols.WSFederation;
using Microsoft.IdentityModel.Web;
using IdP.Security;
using Microsoft.IdentityModel.Protocols.WSFederation.Metadata;
using System.ServiceModel;
using Microsoft.IdentityModel.Protocols.WSIdentity;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.SecurityTokenService;
using System.Xml.Linq;

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

        [HttpGet]
        public void Metadata()
        {
            var config = new SimpleSecurityTokenServiceConfiguration();   

            // Entity
            var ent = new EntityDescriptor();
            ent.Contacts.Add(new ContactPerson(ContactType.Technical) { GivenName = "Sandra", Surname = "Fernandes" });
            ent.EntityId = new EntityId(config.TokenIssuerName);
            ent.SigningCredentials = config.SigningCredentials;

            // Role: Security Token Service
            var role = new SecurityTokenServiceDescriptor();
            var endpoints = new EndpointAddress[] {
                new EndpointAddress("https://idp.prompt11.local:10443/Federation/Issue"),                
            };
            foreach (var ep in endpoints)
            {
                role.SecurityTokenServiceEndpoints.Add(ep);
                role.PassiveRequestorEndpoints.Add(ep);
            }

            role.ServiceDescription = "My Service";
            role.ServiceDisplayName = "My Service Description";
            role.ProtocolsSupported.Add(new Uri("http://docs.oasis-open.org/wsfed/federation/200706"));
            role.ValidUntil = DateTime.Now.AddDays(1);
            role.ClaimTypesOffered.Add(new DisplayClaim(Microsoft.IdentityModel.Claims.ClaimTypes.Role));
            role.ClaimTypesOffered.Add(new DisplayClaim(Microsoft.IdentityModel.Claims.ClaimTypes.Name));

            role.Keys.Add(
                new KeyDescriptor()
                {
                    KeyInfo =
                        new SecurityKeyIdentifier(
                            new X509RawDataKeyIdentifierClause((config.SigningCredentials as X509SigningCredentials).Certificate)
                        ),
                    Use = KeyType.Signing
                }
            );

            ent.RoleDescriptors.Add(role);

            // Serialize
            var serializer = new MetadataSerializer();
            var xe = new XDocument();
            var writer = xe.CreateWriter();
            serializer.WriteMetadata(writer, ent);
            writer.Close();
            Response.ContentType = "text/xml";
            Response.Write(xe.ToString(SaveOptions.DisableFormatting));     
        }
    }
}
