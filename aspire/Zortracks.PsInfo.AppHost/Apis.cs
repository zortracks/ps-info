using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace Zortracks.PsInfo.AppHost {

    public static class Apis {
        public static IResourceBuilder<ProjectResource> ApisResource { get; private set; }

        public static void Configure(IDistributedApplicationBuilder builder) {
            ApisResource = builder.AddProject<Projects.Zortracks_PsInfo_Apis_Host>("apis");
        }
    }
}