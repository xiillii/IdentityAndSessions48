using System.Data.Entity;
using IdentityAndSessions48.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace IdentityAndSessions48.Infrastructure
{
    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<AppIdentityDbContext>
    {
        protected override void Seed(AppIdentityDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        public void PerformInitialSetup(AppIdentityDbContext context)
        {
            var userManager = new AppUserManager(new UserStore<AppUser>(context));
            var roleManager = new AppRoleManager(new RoleStore<AppRole>(context));

            var roleName = "Administrators";
            var userName = "jose";
            var password = "asd,Asd1";
            var email = "mail@josealonso.dev";

            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new AppRole(roleName));
            }

            var user = userManager.FindByName(userName);
            if (user != null)
            {
                user.Email = email;
                user.PasswordHash = userManager.PasswordHasher.HashPassword(password);
                userManager.Update(user);
            }
            else
            {
                userManager.Create(new AppUser { UserName = userName, Email = email }, password);
                user = userManager.FindByName(userName);
            }

            if (!userManager.IsInRole(user.Id, roleName))
            {
                userManager.AddToRole(user.Id, roleName);
            }
        }
    }
}