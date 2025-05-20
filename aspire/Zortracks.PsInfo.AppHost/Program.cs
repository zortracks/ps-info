using Aspire.Hosting;

namespace Zortracks.PsInfo.AppHost {

    public static class Program {

        public static void Main(string[] args) {
            var builder = DistributedApplication.CreateBuilder(args);

            Database.Configure(builder);
            Apis.Configure(builder);
            Application.Configure(builder);

            builder.Build().Run();
        }
    }
}