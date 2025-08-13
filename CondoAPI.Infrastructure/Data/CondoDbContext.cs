using CondoAPI.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CondoAPI.Infrastructure.Data
{
    public class CondoDbContext : DbContext
    {
        public CondoDbContext(DbContextOptions<CondoDbContext> options) : base(options)
        {
        }

        public DbSet<Agent> Agents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>(entity =>
            {
                entity.HasKey(e => e.AgentID);
                entity.ToTable("Agent");
            });

           
        }
    }
}