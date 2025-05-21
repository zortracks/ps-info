using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Zortracks.PsInfo.ServiceDefaults;

namespace Zortracks.PsInfo.AppHost {

    public static class Status {
        public static IResourceBuilder<RedisResource> RedisResource { get; private set; }

        public static void Configure(IDistributedApplicationBuilder builder) {
            RedisResource = builder.AddRedis(ServiceNames.Status.Redis);

            PullingService.Configure(builder);
        }

        public static class PullingService {
            public static IResourceBuilder<ProjectResource> PullingServiceResource { get; private set; }

            public static void Configure(IDistributedApplicationBuilder builder) {
                PullingServiceResource = builder.AddProject<Projects.Zortracks_PsInfo_Status_PullingService>(ServiceNames.Status.PullingService)
                    .WaitFor(RedisResource)
                    .WithReference(RedisResource);
            }
        }
    }
}