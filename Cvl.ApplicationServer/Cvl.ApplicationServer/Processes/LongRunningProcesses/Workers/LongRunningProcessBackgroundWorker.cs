using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cvl.ApplicationServer.Processes.LongRunningProcesses.Workers
{
    public class LongRunningProcessBackgroundWorker : BackgroundService
    {
        private readonly ILogger<LongRunningProcessBackgroundWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public LongRunningProcessBackgroundWorker(ILogger<LongRunningProcessBackgroundWorker> logger, 
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker started at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                var worker = _serviceProvider.GetRequiredService<ILongRunningProcessWorker>();

                var delay = await worker.RunProcessesAsync();

                await Task.Delay(delay, stoppingToken);
            }

            _logger.LogInformation("Worker stopped at: {time}", DateTimeOffset.Now);
        }
    }
}
