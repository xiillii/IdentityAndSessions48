using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityAndSessions48.Controllers
{
    [AllowAnonymous]
    public class KeepAliveController : Controller
    {
        // GET: KeepAlive
        [AllowAnonymous]
        public ActionResult Index()
        {
            return Content("I am alive");
        }
    }
}