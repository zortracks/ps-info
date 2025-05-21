using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using Zortracks.PsInfo.Status.Models;

namespace Zortracks.PsInfo.Status.PullingService {

    public sealed class HealthChecksWorker : BackgroundService {
        private readonly HealthCheckService _healthCheckService;
        private readonly PeriodicTimer _periodicTimer;
        private readonly IOptions<PullingServiceParameter> _pullingServiceParameter;

        public HealthChecksWorker(HealthCheckService healthCheckService, IOptions<PullingServiceParameter> pullingServiceParameter) {
            _healthCheckService = healthCheckService;
            _pullingServiceParameter = pullingServiceParameter;
            _periodicTimer = new PeriodicTimer(_pullingServiceParameter.Value.Period);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            do {
                await _healthCheckService.CheckHealthAsync(stoppingToken);
            }
            while (await _periodicTimer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested);
        }
    }
}