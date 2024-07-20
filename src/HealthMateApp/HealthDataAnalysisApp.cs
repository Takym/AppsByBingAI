using Microsoft.Extensions.Logging;

namespace HealthMateApp
{
    public class HealthDataAnalysisApp : IHealthMateAppCore
    {
        private readonly IHealthDataStorageService _healthDataStorageService;
        private readonly IHealthAnalysisService _healthAnalysisService;
        private readonly ILogger<HealthDataAnalysisApp> _logger;

        public HealthDataAnalysisApp(IHealthDataStorageService healthDataStorageService, IHealthAnalysisService healthAnalysisService, ILogger<HealthDataAnalysisApp> logger)
        {
            _healthDataStorageService = healthDataStorageService;
            _healthAnalysisService = healthAnalysisService;
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogInformation("HealthDataAnalysisApp is running...");
            var healthData = _healthDataStorageService.LoadHealthData();
            if (healthData == null)
            {
                _logger.LogWarning("No health data found.");
                return;
            }

            double bmi = _healthAnalysisService.CalculateBMI(healthData.Height, healthData.Weight);
            string exerciseEvaluation = _healthAnalysisService.EvaluateExercise(healthData.ExerciseMinutes);

            _logger.LogInformation("保存されたデータ:");
            _logger.LogInformation($"名前: {healthData.Name}");
            _logger.LogInformation($"年齢: {healthData.Age}");
            _logger.LogInformation($"身長: {healthData.Height} cm");
            _logger.LogInformation($"体重: {healthData.Weight} kg");
            _logger.LogInformation($"運動時間: {healthData.ExerciseMinutes} 分");

            _logger.LogInformation("分析結果:");
            _logger.LogInformation($"BMI: {bmi:F2}");
            _logger.LogInformation($"運動評価: {exerciseEvaluation}");
        }
    }
}
