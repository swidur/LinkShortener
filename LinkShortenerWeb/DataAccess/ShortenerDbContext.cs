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
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Shortener>()
                .HasIndex(u => u.Slug)
                .IsUnique();
        }
        public DbSet<Shortener> Shortening { get; set; }
    }
}
