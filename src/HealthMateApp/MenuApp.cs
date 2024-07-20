using System;
using Microsoft.Extensions.Logging;

namespace HealthMateApp
{
    public class MenuApp : IHealthMateAppCore
    {
        private readonly IHealthMateAppProvider _appProvider;
        private readonly ILogger<MenuApp> _logger;

        public MenuApp(IHealthMateAppProvider appProvider, ILogger<MenuApp> logger)
        {
            _appProvider = appProvider;
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogInformation("MenuApp is running...");

            Console.WriteLine("Choose an application to run:");
            Console.WriteLine("1. Health Data Input");
            Console.WriteLine("2. Health Data Analysis");
            var choice = Console.ReadLine();

            try
            {
                var app = _appProvider.GetApp(choice);
                app.Run();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Invalid choice");
                Console.WriteLine("Invalid choice. Please restart the application and try again.");
            }
        }
    }
}
