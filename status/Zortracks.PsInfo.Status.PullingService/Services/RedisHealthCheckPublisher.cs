using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zortracks.PsInfo.Status.Models;

namespace Zortracks.PsInfo.Status.PullingService {

    public sealed class RedisHealthCheckPublisher : IHealthCheckPublisher {
        private readonly IOptions<PullingServiceParameter> _pullingServiceParameter;
        private readonly IServiceProvider _serviceProvider;

        public RedisHealthCheckPublisher(IServiceProvider serviceProvider, IOptions<PullingServiceParameter> pullingServiceParameter) {
            _serviceProvider = serviceProvider;
            _pullingServiceParameter = pullingServiceParameter;
        }

        public async Task PublishAsync(HealthReport report, CancellationToken cancellationToken) {
            var serviceProvider = _serviceProvider.CreateScope().ServiceProvider;
            var publishEndpoint = serviceProvider.GetRequiredService<IPublishEndpoint>();

            foreach (var status in report.Entries.Where(entry => _pullingServiceParameter.Value.HealthChecks.Any(healthCheck => healthCheck.Name == entry.Key)))
                await publishEndpoint.Publish(new StatusReport() {
                    Name = status.Key,
                    Status = status.Value.Status,
                    Duration = status.Value.Duration
                }, cancellationToken);
        }
    }
}