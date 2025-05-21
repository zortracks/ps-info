using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Zortracks.PsInfo.ServiceDefaults;

namespace Zortracks.PsInfo.AppHost {

    public static class Landing {
        private static IResourceBuilder<SqlServerDatabaseResource> _landingDatabaseResource;

        public static IResourceBuilder<ProjectResource> ApisResource { get; private set; }
        public static IResourceBuilder<ProjectResource> ApplicationResource { get; private set; }
        public static IResourceBuilder<SqlServerDatabaseResource> LandingDatabaseResource { get => _landingDatabaseResource ??= Database.DatabaseServerResource.AddDatabase(ServiceNames.Landing.Database, "landing"); }

        public static void Configure(IDistributedApplicationBuilder builder) {
            ApisResource = builder.AddProject<Projects.Zortracks_PsInfo_Landing_Apis_Host>(ServiceNames.Landing.Apis)
                .WaitFor(LandingDatabaseResource)
                .WithReference(LandingDatabaseResource);

            ApplicationResource = builder.AddProject<Projects.Zortracks_PsInfo_Landing_Application_Host>(ServiceNames.Landing.Application)
                .WaitFor(ApisResource)
                .WithReference(ApisResource);
        }
    }
}