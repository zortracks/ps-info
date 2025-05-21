using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;

namespace Zortracks.PsInfo.Status.Data.Entities {

    public sealed class EntryEntity {
        public Guid Id { get; set; }
        public DateTime Issued { get; set; }
        public ProjectEntity Project { get; set; }
        public string ProjectName { get; set; }
        public HealthStatus Status { get; set; }
    }
}