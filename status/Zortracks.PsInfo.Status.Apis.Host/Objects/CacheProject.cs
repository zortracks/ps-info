using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Zortracks.PsInfo.Status.Apis.Host.Objects {

    public sealed class CacheProject {
        public string Name { get; set; }
        public HealthStatus Status { get; set; }
    }
}