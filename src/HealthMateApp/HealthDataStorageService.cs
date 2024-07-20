using System;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace HealthMateApp
{
    public class HealthDataStorageService : IHealthDataStorageService
    {
        private const string FilePath = "healthdata.json";
        private readonly ILogger<HealthDataStorageService> _logger;

        public HealthDataStorageService(ILogger<HealthDataStorageService> logger)
        {
            _logger = logger;
        }

        public void SaveHealthData(HealthData healthData)
        {
            try
            {
                var jsonData = JsonSerializer.Serialize(healthData);
                File.WriteAllText(FilePath, jsonData);
                _logger.LogInformation("データが保存されました。");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "データの保存中にエラーが発生しました。");
            }
        }

        public HealthData LoadHealthData()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    _logger.LogWarning("保存されたデータが見つかりません。");
                    return null;
                }

                var jsonData = File.ReadAllText(FilePath);
                var healthData = JsonSerializer.Deserialize<HealthData>(jsonData);
                _logger.LogInformation("データが読み込まれました。");
                return healthData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "データの読み込み中にエラーが発生しました。");
                return null;
            }
        }
    }
}
