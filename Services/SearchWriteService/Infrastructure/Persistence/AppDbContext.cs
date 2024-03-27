using Microsoft.EntityFrameworkCore;
using SearchWriteService.Domain;

namespace SearchWriteService.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public virtual DbSet<SearchResults> SearchResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchResults>().ToTable("SearchResults");
        }
    }
}
