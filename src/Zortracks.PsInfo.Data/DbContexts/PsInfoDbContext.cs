using Microsoft.EntityFrameworkCore;
using Zortracks.PsInfo.Data.Entities;

namespace Zortracks.PsInfo.Data.DbContexts {

    public sealed class PsInfoDbContext : DbContext {

        public PsInfoDbContext(DbContextOptions options) : base(options) {
        }

        public DbSet<ContactRequestEntity> ContactRequests { get; set; }
    }
}