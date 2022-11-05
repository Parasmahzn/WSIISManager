using WSIISManager.Services;

namespace WSIISManager
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IIISServiceExecution _serviceExecution;
        private readonly Iisconfiguration _iisconfiguration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration
            , IEmailService emailService, IIISServiceExecution serviceExecution)
        {
            _logger = logger;
            _configuration = configuration;
            _emailService = emailService;
            _serviceExecution = serviceExecution;
            _iisconfiguration = _configuration.Get<IISConfigModel>().IISConfiguration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await _serviceExecution.StartSitesAsync(_iisconfiguration.Sites);
                await _emailService.SendEmail(_iisconfiguration.Email);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}