using Zortracks.PsInfo.ServiceDefaults;

namespace Zortracks.PsInfo.Status.Apis.Host {

    public static class Program {

        public static void Main(string[] args) {
            /* ========= Configure web application ========= */
            var builder = WebApplication.CreateBuilder(args);

            // Core services
            builder.AddServiceDefaults();
            builder.Services.AddControllers();

            // RabbitMQ services
            builder.AddMassTransitRabbitMq(ServiceNames.Status.RabbitMQ, massTransitConfiguration: configure => {
            });

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