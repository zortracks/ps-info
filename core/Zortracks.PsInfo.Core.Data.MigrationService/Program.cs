using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zortracks.PsInfo.Landing.Data.DbContexts;
using Zortracks.PsInfo.ServiceDefaults;

namespace Zortracks.PsInfo.Core.Data.Migrations {

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