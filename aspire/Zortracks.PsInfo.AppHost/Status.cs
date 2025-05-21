using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Zortracks.PsInfo.ServiceDefaults;

namespace Zortracks.PsInfo.AppHost {

    public static class Status {
        private static IResourceBuilder<SqlServerDatabaseResource> _statusDatabaseResource;

        public static IResourceBuilder<ProjectResource> PullingServiceResource { get; private set; }
        public static IResourceBuilder<RedisResource> RedisResource { get; private set; }
        public static IResourceBuilder<SqlServerDatabaseResource> StatusDatabaseResource { get => _statusDatabaseResource ??= Database.DatabaseServerResource.AddDatabase(ServiceNames.Status.Database, "status"); }

        public static void Configure(IDistributedApplicationBuilder builder) {
            RedisResource = builder.AddRedis(ServiceNames.Status.Redis);

            PullingServiceResource = builder.AddProject<Projects.Zortracks_PsInfo_Status_PullingService>(ServiceNames.Status.PullingService)
                .WaitFor(RedisResource)
                .WithReference(RedisResource);
        }
    }
}