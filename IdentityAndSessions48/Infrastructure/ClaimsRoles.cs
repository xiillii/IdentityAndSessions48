using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace IdentityAndSessions48.Infrastructure
{
    public class ClaimsRoles
    {
        public static IEnumerable<Claim> CreateRolesFromClaims(ClaimsIdentity user)
        {
            var claims = new List<Claim>();
            if (user.HasClaim(x =>
                    x.Type == ClaimTypes.StateOrProvince && x.Issuer == "RemoteClaims" && x.Value == "Pue") &&
                user.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "Employees"))
            {
                claims.Add(new Claim(ClaimTypes.Role, "PueStaff"));
            }

            return claims;
        }
    }
}