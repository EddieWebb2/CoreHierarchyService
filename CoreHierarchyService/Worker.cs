using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CoreHierarchyService.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SerilogHelpers;

namespace CoreHierarchyService
{
    public class Worker : BackgroundService
    {
        private ICoreHierarchyServiceConfiguration _config;
        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<Worker>();


        //private readonly ILogger<Worker> _logger;
        private HttpClient _client;

        public Worker(ILogger<Worker> logger, CoreHierarchyServiceConfiguration config)
        {
            //_logger = logger;

            _config = config;
           

            Logging.InitializeLogging(_config);

            Log.Information("Sample Log output!");

            // We dont need the environment switch anymore!
            // when debugging this will automatically run in console mode, but when deployed will run as service! Cool
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            //_logger.LogInformation($"Worker running at: {DateTime.Now}");

            _client = new HttpClient();
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation($"Worker running at: {DateTime.Now}");

                Log.Information($"Worker running at: {DateTime.Now}");

                var result = await _client.GetAsync("https://www.google.com");

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
