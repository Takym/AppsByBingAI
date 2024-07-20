namespace HealthMateApp
{
    public interface IHealthAnalysisService
    {
        double CalculateBMI(double height, double weight);
        string EvaluateExercise(int exerciseMinutes);
    }
}
