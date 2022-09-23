using System.Text.Json;
using ExtensibleWorkerService.Core;
using ExtensibleWorkerService.Dispatcher;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context,services) => { services.AddWorkerServices(context.Configuration); })
    .Build();
var services = host.Services.GetServices<IHostedService>();
SerializeWorkers(services);
await host.RunAsync();

void SerializeWorkers(IEnumerable<IHostedService> hostedServices)
{
    var workerTasks = hostedServices.Cast<IWorkerTask>();
    var json = JsonSerializer.Serialize(workerTasks);
    File.WriteAllText("runningServices.json", json);
}