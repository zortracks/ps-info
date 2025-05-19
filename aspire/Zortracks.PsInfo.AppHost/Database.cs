using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace Zortracks.PsInfo.AppHost {

    public static class Database {
        public static IResourceBuilder<MySqlServerResource> MySqlServerResource { get; private set; }
        public static IResourceBuilder<MySqlDatabaseResource> PsInfoDatabase { get; private set; }

        public static void Configure(IDistributedApplicationBuilder builder) {
            MySqlServerResource = builder.AddMySql("mysql-server")
                .WithDataVolume("mysql-data")
                .WithPhpMyAdmin(configureContainer => configureContainer.PublishAsContainer());

            PsInfoDatabase = MySqlServerResource.AddDatabase("ps-info-database", "ps-info");
        }
    }
}