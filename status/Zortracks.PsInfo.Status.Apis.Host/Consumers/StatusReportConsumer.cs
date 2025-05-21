using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Zortracks.PsInfo.Status.Apis.Host.Services;
using Zortracks.PsInfo.Status.Models;

namespace Zortracks.PsInfo.Status.Apis.Host.Consumers {

    public sealed class StatusReportConsumer : IConsumer<StatusReport> {
        private readonly IServiceProvider _serviceProvider;

        public StatusReportConsumer(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public Task Consume(ConsumeContext<StatusReport> context) {
            _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<StatusReportsService>().UpdateStatus(context.Message);

            return Task.CompletedTask;
        }
    }
}