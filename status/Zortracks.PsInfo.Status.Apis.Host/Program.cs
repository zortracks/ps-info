using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zortracks.PsInfo.ServiceDefaults;
using Zortracks.PsInfo.Status.Apis.Host.Consumers;
using Zortracks.PsInfo.Status.Apis.Host.Services;
using Zortracks.PsInfo.Status.Data.DbContexts;

namespace Zortracks.PsInfo.Status.Apis.Host {

    public static class Program {

        public static void Main(string[] args) {
            /* ========= Configure web application ========= */
            var builder = WebApplication.CreateBuilder(args);

            // Core services
            builder.AddServiceDefaults();
            builder.Services.AddControllers();
            builder.Services.AddMemoryCache();

            // RabbitMQ services
            builder.AddMassTransitRabbitMq(ServiceNames.Status.RabbitMQ, massTransitConfiguration: configure => {
                configure.AddConsumer<StatusReportConsumer>();
            });

            // Business services
            builder.Services.AddScoped<StatusReportsService>();

            // Database services
            builder.AddSqlServerDbContext<StatusDbContext>(ServiceNames.Status.Database);

            /* ========= Build web application ========= */
            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            /* ========= Run web application ========= */
            app.Run();
        }
    }
}