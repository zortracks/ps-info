using Microsoft.EntityFrameworkCore;

namespace Zortracks.PsInfo.Status.Data.DbContexts {

    public sealed class StatusDbContext : DbContext {

        public StatusDbContext(DbContextOptions options) : base(options) {
        }
    }
}