using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace Zortracks.PsInfo.AppHost {

    public static class Database {
        public static IResourceBuilder<ProjectResource> DataMigrationResource { get; private set; }
        public static IResourceBuilder<SqlServerServerResource> MsSqlServerResource { get; private set; }
        public static IResourceBuilder<SqlServerDatabaseResource> PsInfoDatabaseResource { get; private set; }

        public static void Configure(IDistributedApplicationBuilder builder) {
            MsSqlServerResource = builder.AddSqlServer("mssql-server")
                .WithDataVolume("mssql-server-data")
                .WithLifetime(ContainerLifetime.Persistent);

            PsInfoDatabaseResource = MsSqlServerResource.AddDatabase("ps-info-database", "ps-info");

            DataMigrationResource = builder.AddProject<Projects.Zortracks_PsInfo_Data_Migrations>("ps-info-database-migrations")
                //.WaitFor(PsInfoDatabaseResource)
                .WithReference(PsInfoDatabaseResource);
        }
    }
}