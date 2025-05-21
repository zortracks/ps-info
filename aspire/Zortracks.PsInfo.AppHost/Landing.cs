using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Zortracks.PsInfo.ServiceDefaults;

namespace Zortracks.PsInfo.AppHost {

    public static class Landing {
        public static IResourceBuilder<SqlServerDatabaseResource> LandingDatabaseResource { get; private set; }

        public static void Configure(IDistributedApplicationBuilder builder) {
            LandingDatabaseResource = Database.DatabaseServerResource.AddDatabase(ServiceNames.Landing.Database, "ps-info");

            Apis.Configure(builder);
            Application.Configure(builder);
        }

        public static class Apis {
            public static IResourceBuilder<ProjectResource> ApisResource { get; private set; }

            public static void Configure(IDistributedApplicationBuilder builder) {
                ApisResource = builder.AddProject<Projects.Zortracks_PsInfo_Landing_Apis_Host>(ServiceNames.Landing.Apis)
                    .WaitFor(LandingDatabaseResource)
                    .WithReference(LandingDatabaseResource);
            }
        }

        public static class Application {
            public static IResourceBuilder<ProjectResource> ApplicationResource { get; private set; }

            public static void Configure(IDistributedApplicationBuilder builder) {
                ApplicationResource = builder.AddProject<Projects.Zortracks_PsInfo_Landing_Application_Host>(ServiceNames.Landing.Application)
                    .WaitFor(Apis.ApisResource)
                    .WithReference(Apis.ApisResource);
            }
        }
    }
}