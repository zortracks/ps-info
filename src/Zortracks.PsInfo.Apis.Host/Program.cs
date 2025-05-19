using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Zortracks.PsInfo.Apis.Host {

    public static class Program {

        public static void Main(string[] args) {
            /* ========= Configure web application ========= */
            var builder = WebApplication.CreateBuilder(args);

            // Core services
            builder.AddServiceDefaults();

            /* ========= Build web application ========= */
            var app = builder.Build();

            app.MapDefaultEndpoints();
            app.UseHttpsRedirection();

            /* ========= Run web application ========= */
            app.Run();
        }
    }
}