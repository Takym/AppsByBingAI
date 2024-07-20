using Microsoft.Extensions.Logging;

namespace HealthMateApp
{
    public class App : IHealthMateAppCore
    {
        private readonly IHealthDataService _healthDataService;
        private readonly IHealthAnalysisService _healthAnalysisService;
        private readonly IHealthDataStorageService _healthDataStorageService;
        private readonly ILogger<App> _logger;
        private HealthData _healthData;

        public App(IHealthDataService healthDataService, IHealthAnalysisService healthAnalysisService, IHealthDataStorageService healthDataStorageService, ILogger<App> logger)
        {
            _healthDataService = healthDataService;
            _healthAnalysisService = healthAnalysisService;
            _healthDataStorageService = healthDataStorageService;
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogInformation("HealthMateApp is running...");

            // データの読み込み
            _healthData = _healthDataStorageService.LoadHealthData();
            if (_healthData == null)
            {
                _healthData = _healthDataService.InputHealthData();
                _healthDataStorageService.SaveHealthData(_healthData);
            }

            double bmi = _healthAnalysisService.CalculateBMI(_healthData.Height, _healthData.Weight);
            string exerciseEvaluation = _healthAnalysisService.EvaluateExercise(_healthData.ExerciseMinutes);

            _logger.LogInformation("保存されたデータ:");
            _logger.LogInformation($"名前: {_healthData.Name}");
            _logger.LogInformation($"年齢: {_healthData.Age}");
            _logger.LogInformation($"身長: {_healthData.Height} cm");
            _logger.LogInformation($"体重: {_healthData.Weight} kg");
            _logger.LogInformation($"運動時間: {_healthData.ExerciseMinutes} 分");

            _logger.LogInformation("分析結果:");
            _logger.LogInformation($"BMI: {bmi:F2}");
            _logger.LogInformation($"運動評価: {exerciseEvaluation}");
        }
    }
}
