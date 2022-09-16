using System;
using System.Threading;
using System.Threading.Tasks;
using ExtensibleWorkerService.Core;

namespace ExtensibleWorkerService.WorkerSDK
{
    public class WorkerTemplate : IWorkerTask
    {
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Intervall { get; set; }
        public DateTimeOffset LastExecution { get; set; }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}