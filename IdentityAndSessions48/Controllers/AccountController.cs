using System.Threading.Tasks;
using IdentityAndSessions48.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using IdentityAndSessions48.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;


namespace IdentityAndSessions48.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "/")
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new string[] { "Access Denied" });
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel details, string returnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(details.Name, details.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid name or password");
                }
                else
                {
                    var ident = await UserManager.CreateIdentityAsync(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    ident.AddClaims(LocationClaimsProvider.GetClaims(ident));
                    ident.AddClaims(ClaimsRoles.CreateRolesFromClaims(ident));
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, ident);
                    return Redirect(returnUrl);
                }
            }

            ViewBag.ReturnUrl = returnUrl;

            return View(details);
        }

        [Authorize]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult GoogleLogin(string returnUrl = "/")
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleLoginCallback", new { returnUrl = returnUrl })
            };
            HttpContext.GetOwinContext().Authentication.Challenge(properties, "Google");
            return new HttpUnauthorizedResult();
        }

        [AllowAnonymous]
        public async Task<ActionResult> GoogleLoginCallback(string returnUrl = "/")
        {
            var loginInfo = await AuthManager.GetExternalLoginInfoAsync();
            var user = await UserManager.FindAsync(loginInfo.Login);

            if (user == null)
            {
                user = new AppUser
                {
                    Email = loginInfo.Email,
                    UserName = loginInfo.DefaultUserName,
                    City = Cities.LONDON,
                    Country = Countries.UK
                };

                var result = await UserManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return View("Error", result.Errors);
                }

                result = await UserManager.AddLoginAsync(user.Id, loginInfo.Login);
                if (!result.Succeeded)
                {
                    return View("Error", result.Errors);
                }
            }

            var ident = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            ident.AddClaims(loginInfo.ExternalIdentity.Claims);
            AuthManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = false
            }, ident);

            return Redirect(returnUrl);
        }

        private IAuthenticationManager AuthManager => HttpContext.GetOwinContext().Authentication;

        private AppUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
    }
}