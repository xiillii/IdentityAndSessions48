using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using IdentityAndSessions48.Infrastructure;

namespace IdentityAndSessions48.Controllers
{
    public class ClaimsController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var ident = HttpContext.User.Identity as ClaimsIdentity;
            if (ident == null)
            {
                return View("Error", new string[] { "No claims available" });
            }

            return View(ident.Claims);
        }

        [Authorize(Roles = "PueStaff")]
        public ActionResult OtherAction()
        {
            return View();
        }

        [ClaimsAccess(Issuer = "RemoteClaims", ClaimType = ClaimTypes.PostalCode, Value = "DC 21234")]
        public ActionResult ClaimsAccess()
        {
            return View();
        }
    }
}