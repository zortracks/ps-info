using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zortracks.PsInfo.Status.Models;

namespace Zortracks.PsInfo.Status.PullingService {

    public sealed class RedisHealthCheckPublisher : IHealthCheckPublisher {
        private readonly IOptions<PullingServiceParameter> _pullingServiceParameter;
        private readonly IOutputCacheStore _outputCacheStore;

        public RedisHealthCheckPublisher(IOptions<PullingServiceParameter> pullingServiceParameter, IOutputCacheStore outputCacheStore) {
            _pullingServiceParameter = pullingServiceParameter;
            _outputCacheStore = outputCacheStore;
        }

        public Task PublishAsync(HealthReport report, CancellationToken cancellationToken) {
            foreach (var status in report.Entries.Where(entry => _pullingServiceParameter.Value.HealthChecks.Any(healthCheck => healthCheck.Name == entry.Key))) {
                _outputCacheStore.SetAsync(status.Key)
            }

            return Task.CompletedTask;
        }
    }
}