using Microsoft.Extensions.Logging;

namespace HealthMateApp
{
    public class HealthDataInputApp : IHealthMateAppCore
    {
        private readonly IHealthDataService _healthDataService;
        private readonly IHealthDataStorageService _healthDataStorageService;
        private readonly ILogger<HealthDataInputApp> _logger;

        public HealthDataInputApp(IHealthDataService healthDataService, IHealthDataStorageService healthDataStorageService, ILogger<HealthDataInputApp> logger)
        {
            _healthDataService = healthDataService;
            _healthDataStorageService = healthDataStorageService;
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogInformation("HealthDataInputApp is running...");
            var healthData = _healthDataService.InputHealthData();
            _healthDataStorageService.SaveHealthData(healthData);
        }
    }
}
