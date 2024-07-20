using System;

namespace HealthMateApp
{
    public class HealthAnalysisService : IHealthAnalysisService
    {
        public double CalculateBMI(double height, double weight)
        {
            // 身長をメートルに変換
            double heightInMeters = height / 100;
            // BMIを計算
            return weight / (heightInMeters * heightInMeters);
        }

        public string EvaluateExercise(int exerciseMinutes)
        {
            // 推奨される運動時間（例: 週に150分）
            const int recommendedMinutes = 150;
            if (exerciseMinutes >= recommendedMinutes)
            {
                return "十分な運動をしています。";
            }
            else
            {
                return "もっと運動が必要です。";
            }
        }
    }
}
