using ExtensibleWorkerService.Dispatcher;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddWorkerServices(context.Configuration);
        if(args.Any(x=>x=="--api"))
        {
            Console.WriteLine("ExtensibleWorker Service with API");
            services.AddHostedService<DispatcherApi>();
        }
    })
    .Build();
var services = host.Services.GetServices<IHostedService>();
if(args.Any(x=>x =="--file"))
{
    Console.WriteLine("Writing Services to runningServices.json");
    File.WriteAllText("runningServices.json",
        SerializationHelper.SerializeWorkers(services));
}
await host.RunAsync();

