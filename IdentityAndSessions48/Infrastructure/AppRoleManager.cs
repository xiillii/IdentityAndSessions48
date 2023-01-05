using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityAndSessions48.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace IdentityAndSessions48.Infrastructure
{
    public class AppRoleManager : RoleManager<AppRole>
    {
        public AppRoleManager(IRoleStore<AppRole, string> store) : base(store)
        {
        }

        public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options, IOwinContext context) =>
            new AppRoleManager(new RoleStore<AppRole>(context.Get<AppIdentityDbContext>()));
    }
}