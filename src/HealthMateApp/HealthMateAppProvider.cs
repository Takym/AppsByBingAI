using System;
using Microsoft.Extensions.DependencyInjection;

namespace HealthMateApp
{
    public class HealthMateAppProvider : IHealthMateAppProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public HealthMateAppProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IHealthMateAppCore GetApp(string choice)
        {
            return choice switch
            {
                "1" => _serviceProvider.GetRequiredService<HealthDataInputApp>(),
                "2" => _serviceProvider.GetRequiredService<HealthDataAnalysisApp>(),
                _ => throw new InvalidOperationException("Invalid choice")
            };
        }
    }
}
