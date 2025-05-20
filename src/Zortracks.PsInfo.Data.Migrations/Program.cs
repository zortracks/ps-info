using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zortracks.PsInfo.Data.DbContexts;

namespace Zortracks.PsInfo.Data.Migrations {

    public static class Program {

        public static void Main(string[] args) {
            /* ========= Configure worker ========= */
            var builder = Host.CreateApplicationBuilder(args);

            // Core services
            builder.AddServiceDefaults();
            builder.Services.Configure<HostOptions>(options => options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.StopHost);

            // Worker services
            builder.Services.AddHostedService<MigrationWorker>();

            // Database services
            builder.AddSqlServerDbContext<PsInfoDbContext>("ps-info-database");

            /* ========= Build and run worker ========= */
            builder.Build().Run();
        }
    }
}