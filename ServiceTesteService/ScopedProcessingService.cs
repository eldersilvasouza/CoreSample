using Microsoft.Extensions.Logging;
using ServiceTesteService.Interface;

namespace ServiceTesteService
{
    public class ScopedProcessingService : IScopedProcessingService
    {
        private readonly ILogger _logger;

        public ScopedProcessingService(ILogger logger)
        {
            _logger = logger;
        }

        public void DoWork()
        {
            _logger.LogInformation("Escopo do serviço sendo processado.");
        }
    }
}
