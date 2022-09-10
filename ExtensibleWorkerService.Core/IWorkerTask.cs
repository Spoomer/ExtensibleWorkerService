using System;
using Microsoft.Extensions.Hosting;

namespace ExtensibleWorkerService.Core
{
    public interface IWorkerTask : IHostedService
    {
        Guid Id { get; set; }
        string Name { get; set; }
        TimeSpan Intervall { get; set; }
        DateTimeOffset LastExecution { get; set; }
    }
}