using Microsoft.AspNet.Identity;

namespace IdentityAndSessions48.Migrations
{
    using IdentityAndSessions48.Infrastructure;
    using IdentityAndSessions48.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IdentityAndSessions48.Infrastructure.AppIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "IdentityAndSessions48.Infrastructure.AppIdentityDbContext";
        }

        protected override void Seed(IdentityAndSessions48.Infrastructure.AppIdentityDbContext context)
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

            foreach (var dbUser in userManager.Users)
            {
                if (dbUser.Country == Countries.NONE)
                    dbUser.SetCountryFromCity(dbUser.City);
            }

            context.SaveChanges();
        }
    }
}
