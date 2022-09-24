using System.Text.Json;
using System.Text.Json.Serialization;
using ExtensibleWorkerService.Core;

namespace ExtensibleWorkerService.Dispatcher;

public class SerializationHelper
{
    public static string SerializeWorkers(IEnumerable<IHostedService> hostedServices)
    {
        var workerTasks = hostedServices
            .Select(x => x as IWorkerTask)
            .Where(x => x is not null);
        
        var statusObj = new {Created = DateTimeOffset.Now, Services = workerTasks};
        
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull};
        
        return JsonSerializer.Serialize(statusObj, options);
    }
}