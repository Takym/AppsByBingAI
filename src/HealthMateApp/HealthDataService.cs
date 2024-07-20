using System;

namespace HealthMateApp
{
    public class HealthData
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public int ExerciseMinutes { get; set; }
    }

    public class HealthDataService : IHealthDataService
    {
        public HealthData InputHealthData()
        {
            Console.WriteLine("健康データを入力してください。");

            Console.Write("名前: ");
            string name = Console.ReadLine();

            Console.Write("年齢: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("身長 (cm): ");
            double height = double.Parse(Console.ReadLine());

            Console.Write("体重 (kg): ");
            double weight = double.Parse(Console.ReadLine());

            Console.Write("運動時間 (分): ");
            int exerciseMinutes = int.Parse(Console.ReadLine());

            return new HealthData
            {
                Name = name,
                Age = age,
                Height = height,
                Weight = weight,
                ExerciseMinutes = exerciseMinutes
            };
        }
    }
}
