using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace Zortracks.PsInfo.AppHost {

    public static class Application {
        public static IResourceBuilder<ProjectResource> ApplicationResource { get; private set; }

        public static void Configure(IDistributedApplicationBuilder builder) {
            ApplicationResource = builder.AddProject<Projects.Zortracks_PsInfo_Application_Host>("application")
                .WaitFor(Apis.ApisResource)
                .WithReference(Apis.ApisResource);
        }
    }
}