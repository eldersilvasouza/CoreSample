using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceTesteService.Interface;

namespace ServiceTesteService
{
    internal class ConsumeScopedServiceHostedService : IHostedService
    {
        private readonly ILogger _logger;

        public IServiceProvider Services { get; }


        public ConsumeScopedServiceHostedService(IServiceProvider services, ILogger<ConsumeScopedServiceHostedService> logger)
        {
            Services = services;
            _logger = logger;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Serviço consumido no escopo do Hosted Service e sendo iniciado");

            DoWork();

            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consumo do serviço stopado!");

            return Task.CompletedTask;
        }


        private void DoWork()
        {

            _logger.LogInformation("Serviço  iniciado");

            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IScopedProcessingService>();

                scopedProcessingService.DoWork();

            }

        }






    }
}
