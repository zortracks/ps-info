using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor;
using MudBlazor.Services;
using System;
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
            builder.Services.AddLocalization();
            builder.Services.AddHttpClient("apis", configure => configure.BaseAddress = builder.Configuration.GetValue<Uri>("services:apis:http:0"));

            /* ========= Build web application ========= */
            var app = builder.Build();

            app.UseHsts();
            app.UseRequestLocalization();
            app.UseHttpsRedirection();
            app.UseAntiforgery();
            app.MapStaticAssets();
            app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

            /* ========= Run web application ========= */
            app.Run();
        }
    }
}