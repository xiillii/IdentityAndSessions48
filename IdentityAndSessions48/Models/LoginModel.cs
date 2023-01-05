using System.ComponentModel.DataAnnotations;

namespace IdentityAndSessions48.Models
{
    public class LoginModel
    {
        [Required] public string Name { get; set; }
        [Required] public string Password { get; set;  }
    }
}