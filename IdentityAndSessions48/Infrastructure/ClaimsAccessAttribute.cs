using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace IdentityAndSessions48.Infrastructure
{
    public class ClaimsAccessAttribute : AuthorizeAttribute
    {
        public string Issuer { get; set; }
        public string ClaimType { get; set; }
        public string Value { get; set; }

        protected override bool AuthorizeCore(HttpContextBase context) =>
            context.User.Identity.IsAuthenticated && context.User.Identity is ClaimsIdentity identity &&
            identity.HasClaim(x =>
                x.Issuer == Issuer && x.Type == ClaimType && x.Value == Value);
    }
}