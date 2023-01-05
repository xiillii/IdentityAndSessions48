using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityAndSessions48.Models
{
    public class AppUser : IdentityUser
    {
        public Cities City { get; set; }
        public Countries Country { get; set; }

        public void SetCountryFromCity(Cities city)
        {
            switch (city)
            {
                case Cities.LONDON:
                    Country = Countries.UK;
                    break;
                case Cities.PARIS:
                    Country = Countries.FRANCE;
                    break;
                case Cities.CHICAGO:
                    Country = Countries.USA;
                    break;
                case Cities.PUEBLA:
                    Country = Countries.MEXICO;
                    break;
                default:
                    Country = Countries.NONE;
                    break;
            }
        }
    }
}