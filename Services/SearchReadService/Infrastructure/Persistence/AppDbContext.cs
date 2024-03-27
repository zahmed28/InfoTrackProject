using Microsoft.EntityFrameworkCore;
using SearchReadService.Domain;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SearchReadService.Infrastructure.Persistence
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
