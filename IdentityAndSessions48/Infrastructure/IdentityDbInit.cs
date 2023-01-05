using System.Data.Entity;
using IdentityAndSessions48.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace IdentityAndSessions48.Infrastructure
{
    public class IdentityDbInit : NullDatabaseInitializer<AppIdentityDbContext>
    {
        
    }
}