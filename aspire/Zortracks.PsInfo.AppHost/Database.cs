using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace Zortracks.PsInfo.AppHost {

    public static class Database {
        public static IResourceBuilder<SqlServerServerResource> MsSqlServerResource { get; private set; }
        public static IResourceBuilder<SqlServerDatabaseResource> PsInfoDatabase { get; private set; }

        public static void Configure(IDistributedApplicationBuilder builder) {
            MsSqlServerResource = builder.AddSqlServer("mssql-server")
                .WithDataVolume("mssql-server-data")
                .WithLifetime(ContainerLifetime.Persistent);

            PsInfoDatabase = MsSqlServerResource.AddDatabase("ps-info-database", "ps-info");
        }
    }
}