using Microsoft.EntityFrameworkCore;
using Zortracks.PsInfo.Landing.Data.Entities;

namespace Zortracks.PsInfo.Landing.Data.DbContexts {

    public sealed class PsInfoDbContext : DbContext {

        public PsInfoDbContext(DbContextOptions options) : base(options) {
        }

        public DbSet<ContactRequestEntity> ContactRequests { get; set; }
    }
}