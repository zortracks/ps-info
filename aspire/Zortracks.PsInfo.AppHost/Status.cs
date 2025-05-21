using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using JsonFlatten;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using Zortracks.PsInfo.ServiceDefaults;
using Zortracks.PsInfo.Status.Models;

namespace Zortracks.PsInfo.AppHost {

    public static class Status {
        private static IResourceBuilder<SqlServerDatabaseResource> _statusDatabaseResource;

        public static IResourceBuilder<ProjectResource> ApiResource { get; private set; }
        public static IResourceBuilder<ProjectResource> PullingServiceResource { get; private set; }
        public static IResourceBuilder<RabbitMQServerResource> RabbitMQResource { get; private set; }
        public static IResourceBuilder<SqlServerDatabaseResource> StatusDatabaseResource { get => _statusDatabaseResource ??= Database.DatabaseServerResource.AddDatabase(ServiceNames.Status.Database, "status"); }

        public static void Configure(IDistributedApplicationBuilder builder) {
            /* ========= RabbitMQ ========= */
            RabbitMQResource = builder.AddRabbitMQ(ServiceNames.Status.RabbitMQ);

            if (builder.Environment.IsDevelopment())
                RabbitMQResource = RabbitMQResource.WithManagementPlugin();

            RabbitMQResource = RabbitMQResource.WithLifetime(ContainerLifetime.Persistent);

            /* ========= Pulling service ========= */
            PullingServiceResource = builder.AddProject<Projects.Zortracks_PsInfo_Status_PullingService>(ServiceNames.Status.PullingService)
                // Working references
                .WaitFor(RabbitMQResource)
                .WithReference(RabbitMQResource)

                // Status references
                .WithReference(Landing.LandingDatabaseResource)

                .WithEnvironment(callback => JObject.FromObject(new PullingServiceParameter() {
                    HealthChecks = [
                        new HealthCheck() {
                            Name = ServiceNames.Landing.Database,
                            Kind = HealthCheckKind.SqlServer,
                            ConnectionString = ServiceNames.Landing.Database,
                            ConnectionStringType = ConnectionStringType.Key
                        }
                    ],
                    Period = TimeSpan.FromSeconds(10)
                }).Flatten().ToList().ForEach(pair => callback.EnvironmentVariables.Add($"Parameter__{FormatPairKey(pair.Key)}", pair.Value)));

            /* ========= APIs ========= */
            ApiResource = builder.AddProject<Projects.Zortracks_PsInfo_Status_Apis_Host>(ServiceNames.Status.Apis)
                .WaitFor(StatusDatabaseResource)
                .WithReference(StatusDatabaseResource)
                .WaitFor(RabbitMQResource)
                .WithReference(RabbitMQResource);
        }

        private static string FormatPairKey(string key) => key
            .Replace("[", "__")
            .Replace("].", "__")
            .Replace(".", "__");
    }
}