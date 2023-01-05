using IdentityAndSessions48.Infrastructure;
using IdentityAndSessions48.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IdentityAndSessions48.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            
            return View(GetData("Index"));
        }

        [Authorize(Roles = "Users")]
        public ActionResult OtherAction()
        {
            return View("Index", GetData("OtherAction"));
        }

        [Authorize]
        public ActionResult UserProps()
        {
            return View(CurrentUser);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> UserProps(Cities city)
        {
            AppUser user = CurrentUser;
            user.City = city;
            user.SetCountryFromCity(city);
            await UserManager.UpdateAsync(user);
            return View(user);
        }
        private AppUser CurrentUser
        {
            get
            {
                return UserManager.FindByName(HttpContext.User.Identity.Name);
            }
        }
        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private Dictionary<string, object> GetData(string actionName)
        {
            var dict = new Dictionary<string, object>
            {
                { "Action", actionName },
                { "User", HttpContext.User.Identity.Name },
                { "Authenticated", HttpContext.User.Identity.IsAuthenticated },
                { "Auth Type", HttpContext.User.Identity.AuthenticationType },
                { "In Users Role", HttpContext.User.IsInRole("Users") }
            };

            return dict;
        }
    }
}