﻿using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityAndSessions48.Models
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }

        public AppRole(string name) : base(name)
        {
            
        }
    }
}