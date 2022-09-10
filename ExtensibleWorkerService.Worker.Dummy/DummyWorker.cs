using System;
using System.Threading;
using System.Threading.Tasks;
using ExtensibleWorkerService.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ExtensibleWorkerService.Worker.Dummy
{
    public class DummyWorker : BackgroundService, IWorkerTask
    {
        public Guid Id { get; set; } = new Guid("A0DECD61-B144-42D3-B4AB-3B38DBF1DF7C");
        public string Name { get; set; } = "DummyWorker";
        public TimeSpan Intervall { get; set; } = TimeSpan.FromSeconds(1);
        public DateTimeOffset LastExecution { get; set; } = DateTimeOffset.MinValue;

        private readonly ILogger<DummyWorker> _logger;

        public DummyWorker(ILogger<DummyWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker [{Name}] running at: {Time}", Name, DateTimeOffset.Now);
                LastExecution = DateTimeOffset.Now;
                await Task.Delay(Intervall, stoppingToken);
            }
        }
    }
}