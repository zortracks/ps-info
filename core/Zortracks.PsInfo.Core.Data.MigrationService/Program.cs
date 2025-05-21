using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zortracks.PsInfo.Landing.Data.DbContexts;
using Zortracks.PsInfo.ServiceDefaults;
using Zortracks.PsInfo.Status.Data.DbContexts;

namespace Zortracks.PsInfo.Core.Data.Migrations {

    public static class Program {

        public static void Main(string[] args) {
            /* ========= Configure worker ========= */
            var builder = Host.CreateApplicationBuilder(args);

            // Core services
            builder.AddServiceDefaults();
            builder.Services.Configure<HostOptions>(options => options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.StopHost);

            // Worker services
            builder.Services.AddHostedService<MigrationWorker<LandingDbContext>>();
            builder.Services.AddHostedService<MigrationWorker<StatusDbContext>>();

            // Database services
            builder.AddSqlServerDbContext<LandingDbContext>(ServiceNames.Landing.Database);
            builder.AddSqlServerDbContext<StatusDbContext>(ServiceNames.Status.Database);

            /* ========= Build and run worker ========= */
            builder.Build().Run();
        }
    }
}