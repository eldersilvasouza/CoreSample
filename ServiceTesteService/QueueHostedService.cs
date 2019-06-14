using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceTesteService.Interface;

namespace ServiceTesteService
{
    public class QueueHostedService : BackgroundService
    {
        private readonly ILogger _logger;
        public IBackgroundTasksQueue TaskQueue { get; }

        public QueueHostedService(ILoggerFactory loggerFactory, IBackgroundTasksQueue taskQueue)
        {
            _logger = loggerFactory.CreateLogger<QueueHostedService>();
            TaskQueue = taskQueue;
        }       




        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _logger.LogInformation("Queued Hosted Service iniciando");

            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await TaskQueue.DequeueAsync(stoppingToken);

                try
                {
                    await workItem(stoppingToken);
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, $"Error ao executar{nameof(workItem)}.");
                    
                }
            }

            _logger.LogInformation("Agendamento do hosted service stopado");

        }
    }
}
