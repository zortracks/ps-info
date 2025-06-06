using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zortracks.PsInfo.Data.DbContexts;

namespace Zortracks.PsInfo.Apis.Host {

    public static class Program {

        public static void Main(string[] args) {
            /* ========= Configure web application ========= */
            var builder = WebApplication.CreateBuilder(args);

            // Core services
            builder.AddServiceDefaults();
            builder.Services.AddControllers();

            // Database services
            builder.AddSqlServerDbContext<PsInfoDbContext>("ps-info-database");

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