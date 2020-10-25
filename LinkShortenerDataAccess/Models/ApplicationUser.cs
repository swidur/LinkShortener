using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkShortenerDataAccess.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Shortening> UserShortenings { get; set; }
    }
}
