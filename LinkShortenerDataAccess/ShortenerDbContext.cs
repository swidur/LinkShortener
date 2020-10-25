using LinkShortenerDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShortenerDataAccess
{
    public class ShortenerDbContext : DbContext
    {
        public ShortenerDbContext(DbContextOptions<ShortenerDbContext> options) :
            base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Shortening>()
                .HasIndex(u => u.Slug)
                .IsUnique();
        }
        public DbSet<Shortening> Shortening { get; set; }
    }
}
