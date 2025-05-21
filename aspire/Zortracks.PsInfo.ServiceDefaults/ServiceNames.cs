namespace Zortracks.PsInfo.ServiceDefaults {

    public static class ServiceNames {
        public const string DatabaseMigrationService = "mssql-server-migration-service";
        public const string DatabaseServer = "mssql-server";

        public static class Landing {
            public const string Apis = $"{LandingPrefix}apis";
            public const string Application = $"{LandingPrefix}application";
            public const string Database = $"{LandingPrefix}-database";

            private const string LandingPrefix = "landing-";
        }
    }
}