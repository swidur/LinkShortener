using LinkShortenerDataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LinkShortenerDataAccess
{
    public class ShortenerDbContext : IdentityDbContext
    {
        public ShortenerDbContext(DbContextOptions<ShortenerDbContext> options) :
            base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Shortening>()
                .HasIndex(u => u.Slug)
                .IsUnique();
        }
        public DbSet<Shortening> Shortening { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
