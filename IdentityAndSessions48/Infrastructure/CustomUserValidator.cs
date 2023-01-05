using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using IdentityAndSessions48.Models;
using Microsoft.AspNet.Identity;

namespace IdentityAndSessions48.Infrastructure
{
    public class CustomUserValidator : UserValidator<AppUser>
    {
        public CustomUserValidator(AppUserManager manager) : base(manager)
        {
        }

        public override async Task<IdentityResult> ValidateAsync(AppUser user)
        {
            var result = await base.ValidateAsync(user);

            if (!user.Email.ToLower().EndsWith("@josealonso.dev"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Only josealonso.dev email addresses are allowed");
                result = new IdentityResult(errors);
            }

            return result;
        }
    }
}