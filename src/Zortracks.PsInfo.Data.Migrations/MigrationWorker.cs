using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zortracks.PsInfo.Data.DbContexts;

namespace Zortracks.PsInfo.Data.Migrations {

    public sealed class MigrationWorker : BackgroundService {
        private readonly IServiceProvider _serviceProvider;

        public MigrationWorker(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            var database = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<PsInfoDbContext>();

            await database.Database.CreateExecutionStrategy().ExecuteAsync(async () => {
                if ((await database.Database.GetPendingMigrationsAsync(stoppingToken)).Any())
                    await database.Database.MigrateAsync(stoppingToken);
            });
        }
    }
}