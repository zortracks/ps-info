using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;

namespace Zortracks.PsInfo.Status.Models {

    public sealed class StatusReport {
        public TimeSpan Duration { get; set; }
        public string Name { get; set; }
        public HealthStatus Status { get; set; }
        public DateTime Issued { get; set; }
    }
}