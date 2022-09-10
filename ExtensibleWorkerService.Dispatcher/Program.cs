using ExtensibleWorkerService.Dispatcher;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context,services) => { services.AddWorkerServices(context.Configuration); })
    .Build();

await host.RunAsync();