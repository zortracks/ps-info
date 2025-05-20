using Microsoft.EntityFrameworkCore;

namespace Zortracks.PsInfo.Data.DbContexts {

    public sealed class PsInfoDbContext : DbContext {

        public PsInfoDbContext(DbContextOptions options) : base(options) {
        }
    }
}