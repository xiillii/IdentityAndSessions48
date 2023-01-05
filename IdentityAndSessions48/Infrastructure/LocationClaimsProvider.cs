using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace IdentityAndSessions48.Infrastructure
{
    public static class LocationClaimsProvider
    {
        public static IEnumerable<Claim> GetClaims(ClaimsIdentity user)
        {
            var claims = new List<Claim>();
            if (user.Name.ToLower() == "jose")
            {
                claims.Add(CreateClaim(ClaimTypes.PostalCode, "12342"));
                claims.Add(CreateClaim(ClaimTypes.StateOrProvince, "Pue"));
            }
            else
            {
                claims.Add(CreateClaim(ClaimTypes.PostalCode, "NY 10021"));
                claims.Add(CreateClaim(ClaimTypes.StateOrProvince, "NY"));
            }

            return claims;
        }

        private static Claim CreateClaim(string type, string value) =>
            new Claim(type, value, ClaimValueTypes.String, "RemoteClaims");
    }
}