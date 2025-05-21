using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Zortracks.PsInfo.Status.Models {

    public sealed class StatusChanged {
        public string Name { get; set; }
        public HealthStatus CurrentStatus { get; set; }
        public HealthStatus NewStatus { get; set; }
    }
}