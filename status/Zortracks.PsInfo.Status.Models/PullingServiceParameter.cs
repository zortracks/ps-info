using System;
using System.Collections.Generic;

namespace Zortracks.PsInfo.Status.Models {

    public sealed class PullingServiceParameter {
        public IEnumerable<HealthCheck> HealthChecks { get; set; }
        public TimeSpan Period { get; set; }
    }
}