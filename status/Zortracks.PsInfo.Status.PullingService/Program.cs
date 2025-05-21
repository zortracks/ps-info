using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zortracks.PsInfo.ServiceDefaults;

namespace Zortracks.PsInfo.Status.PullingService {

    public static class Program {

        public static void Main(string[] args) {
            /* ========= Configure worker ========= */
            var builder = Host.CreateApplicationBuilder(args);

            // Core services
            builder.AddServiceDefaults();
            builder.Services.Configure<HostOptions>(options => options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.StopHost);

            // Worker services

            /* ========= Build and run worker ========= */
            builder.Build().Run();
        }
    }
}