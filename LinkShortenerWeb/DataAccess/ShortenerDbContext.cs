using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortenerWeb.DataAccess
{
    public class ShortenerDbContext : DbContext
    {
        public ShortenerDbContext(DbContextOptions<ShortenerDbContext> options) :
            base(options)
        {
        }

        public DbSet<Shortening> Shortening { get; set; }
    }
}
