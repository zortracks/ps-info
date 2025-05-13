using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor;
using MudBlazor.Services;
using Zortracks.PsInfo.Application.Host.Shared.Components;

namespace Zortracks.PsInfo.Application.Host {

    public static class Program {

        public static void Main(string[] args) {
            /* ========= Configure web application ========= */
            var builder = WebApplication.CreateBuilder(args);

            // Core services
            builder.AddServiceDefaults();
            builder.Services.AddMudServices(configure => {
                configure.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
            });
            builder.Services.AddRazorComponents().AddInteractiveServerComponents();

            /* ========= Build web application ========= */
            var app = builder.Build();

            if (!app.Environment.IsDevelopment()) {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAntiforgery();
            app.MapStaticAssets();
            app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

            /* ========= Run web application ========= */
            app.Run();
        }
    }
}