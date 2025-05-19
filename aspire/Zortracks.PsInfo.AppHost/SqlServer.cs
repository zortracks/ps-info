using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace Zortracks.PsInfo.AppHost {

    public static class SqlServer {
        public static IResourceBuilder<SqlServerDatabaseResource> PsInfoSqlDatabaseResource { get; private set; }
        public static IResourceBuilder<SqlServerServerResource> SqlServerResource { get; private set; }

        public static void Configure(IDistributedApplicationBuilder builder) {
            SqlServerResource = builder.AddSqlServer("sql-server")
                .WithDataVolume("sql-server-data")
                .WithLifetime(ContainerLifetime.Persistent);

            PsInfoSqlDatabaseResource = SqlServerResource.AddDatabase("sql-database", "psinfo");
        }
    }
}