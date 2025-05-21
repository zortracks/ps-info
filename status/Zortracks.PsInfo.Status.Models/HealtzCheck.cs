namespace Zortracks.PsInfo.Status.Models {

    public sealed class HealthCheck {
        public string ConnectionString { get; set; }
        public ConnectionStringType ConnectionStringType { get; set; } = ConnectionStringType.Raw;
        public HealthCheckKind Kind { get; set; }
        public string Name { get; set; }
    }
}