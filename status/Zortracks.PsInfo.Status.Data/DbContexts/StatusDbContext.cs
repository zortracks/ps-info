using Microsoft.EntityFrameworkCore;
using Zortracks.PsInfo.Status.Data.Entities;

namespace Zortracks.PsInfo.Status.Data.DbContexts {

    public sealed class StatusDbContext : DbContext {

        public StatusDbContext(DbContextOptions options) : base(options) {
        }

        public DbSet<EntryEntity> Entries { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<ProjectEntity>().HasMany(e => e.Entries).WithOne(e => e.Project).HasPrincipalKey(e => e.Name).HasForeignKey(e => e.ProjectName).OnDelete(DeleteBehavior.Cascade);
        }
    }
}