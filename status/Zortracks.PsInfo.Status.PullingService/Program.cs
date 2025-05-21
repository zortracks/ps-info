using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using System;
using Zortracks.PsInfo.ServiceDefaults;
using Zortracks.PsInfo.Status.Models;

namespace Zortracks.PsInfo.Status.PullingService {

    public static class Program {

        public static void Main(string[] args) {
            /* ========= Configure worker ========= */
            var builder = Host.CreateApplicationBuilder(args);

            // Core services
            builder.AddServiceDefaults();
            builder.Services.Configure<HostOptions>(options => options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.StopHost);
            builder.AddRedisOutputCache(ServiceNames.Status.Redis);

            // Health check services
            builder.Services.AddSingleton<IHealthCheckPublisher, RedisHealthCheckPublisher>();

            var healthChecksBuilder = builder.Services.AddHealthChecks();
            var parameterSection = builder.Configuration.GetSection("Parameter");
            var parameter = parameterSection.Get<PullingServiceParameter>();

            foreach (var healthCheck in parameter.HealthChecks) {
                switch (healthCheck.Kind) {
                    case HealthCheckKind.SqlServer:
                        healthChecksBuilder.AddSqlServer(connectionString: healthCheck.ConnectionStringType switch {
                            ConnectionStringType.Key => builder.Configuration.GetConnectionString(healthCheck.ConnectionString),
                            ConnectionStringType.Raw => healthCheck.ConnectionString,
                            _ => throw new NotSupportedException($"Connection string type '{healthCheck.ConnectionStringType}' is not supported."),
                        },
                        name: healthCheck.Name);
                        break;

                    default:
                        break;
                }
            }

            builder.Services.Configure<PullingServiceParameter>(parameterSection.Bind);
            builder.Services.AddHostedService<HealthChecksWorker>();

            /* ========= Build and run worker ========= */
            builder.Build().Run();
        }
    }
}