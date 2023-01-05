using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAndSessions48.Infrastructure
{
    public class CustomPasswordValidator : PasswordValidator
    {
        public override async Task<IdentityResult> ValidateAsync(string pass)
        {
            var result = await base.ValidateAsync(pass);
            if (pass.Contains("12345"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Password cannot contain numeric sequences");
                result = new IdentityResult(errors);
            }

            return result;
        }
    }
}