using System.Reflection;
using ExtensibleWorkerService.Core;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExtensibleWorkerService.Dispatcher;

public static class WorkerServiceCollectionExtensions
{
    public static IServiceCollection AddWorkerServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        IList<Assembly> assemblies = AssemblyHelper.GetLoadedAssemblies(configuration["WorkerDirectory"]);
        IEnumerable<Type> servicesToBeRegistered = assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsAssignableTo(typeof(IWorkerTask)) && type.IsAssignableTo(typeof(IHostedService)));

        foreach (Type serviceType in servicesToBeRegistered)
        {
            serviceCollection.TryAddEnumerable(ServiceDescriptor.Singleton(typeof(IHostedService), serviceType));
        }

        return serviceCollection;
    }
}