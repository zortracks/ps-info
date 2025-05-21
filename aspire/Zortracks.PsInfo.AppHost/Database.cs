using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Zortracks.PsInfo.ServiceDefaults;

namespace Zortracks.PsInfo.AppHost {

    public static class Database {
        public static IResourceBuilder<ProjectResource> DatabaseMigrationServiceResource { get; private set; }
        public static IResourceBuilder<SqlServerServerResource> DatabaseServerResource { get; private set; }

        public static void Configure(IDistributedApplicationBuilder builder) {
            DatabaseServerResource = builder.AddSqlServer(ServiceNames.DatabaseServer)
                .WithDataVolume("mssql-server-data")
                .WithLifetime(ContainerLifetime.Persistent);

            DatabaseMigrationServiceResource = builder.AddProject<Projects.Zortracks_PsInfo_Core_Data_MigrationService>(ServiceNames.DatabaseMigrationService)
                .WaitFor(Landing.LandingDatabaseResource)
                .WithReference(Landing.LandingDatabaseResource)
                .WaitFor(Status.StatusDatabaseResource)
                .WithReference(Status.StatusDatabaseResource);
        }
    }
}