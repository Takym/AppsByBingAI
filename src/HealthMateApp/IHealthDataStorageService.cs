namespace HealthMateApp
{
    public interface IHealthDataStorageService
    {
        void SaveHealthData(HealthData healthData);
        HealthData LoadHealthData();
    }
}
