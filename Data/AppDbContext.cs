using Microsoft.EntityFrameworkCore;
using Aplikacja_RazorPages.Repositories;


namespace Aplikacja_RazorPages.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Painting> Paintings { get; set; }

        // Override OnConfiguring to provide a fallback configuration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
                optionsBuilder.UseSqlite(PaintingRepository.connectionString);
        }
    }
}

