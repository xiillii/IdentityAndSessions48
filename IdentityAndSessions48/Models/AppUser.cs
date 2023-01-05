using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityAndSessions48.Models
{
    public class AppUser : IdentityUser
    {
        public Cities City { get; set; }
    }
}