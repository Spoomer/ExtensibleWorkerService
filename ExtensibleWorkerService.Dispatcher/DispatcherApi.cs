using System.Net;
using System.Text;

namespace ExtensibleWorkerService.Dispatcher;

public class DispatcherApi : BackgroundService
{
        private readonly ILogger<DispatcherApi> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _services;
        private readonly HttpListener _httpListener;


        public DispatcherApi(ILogger<DispatcherApi> logger, IConfiguration configuration, IServiceProvider services)
        {
            _logger = logger;
            _configuration = configuration;
            _services = services;
            _httpListener = new HttpListener();
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _httpListener.Prefixes.Add(
                _configuration[ConfigConstants.ApiEndpoint]+"/services/");

            _httpListener.Start();
            _logger.LogInformation($"DispatcherApi listening...");

            while (!stoppingToken.IsCancellationRequested)
            {
                HttpListenerContext? ctx = null;
                try
                {
                    ctx = await _httpListener.GetContextAsync();
                }
                catch (HttpListenerException ex)
                {
                    if (ex.ErrorCode == 995) return;
                }

                if (ctx == null) continue;

                var response = ctx.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int) HttpStatusCode.OK;

                var messageBytes = Encoding.UTF8.GetBytes(SerializationHelper.SerializeWorkers(_services.GetServices<IHostedService>()));
                response.ContentLength64 = messageBytes.Length;
                await response.OutputStream.WriteAsync(messageBytes, 0, messageBytes.Length, stoppingToken);
                response.OutputStream.Close();
                response.Close();
            }
        }


}