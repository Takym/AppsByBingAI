using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HealthMateApp
{
    class Program
    {
		static async Task Main(string[] args)
		{
		    var host = CreateHostBuilder(args).Build();
		    var services = host.Services;

		    var app = services.GetRequiredService<IHealthMateAppCore>();
		    app.Run();

		    // ホストを停止
		    await host.StopAsync();
		}

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IHealthMateAppCore, MenuApp>();
                    services.AddTransient<IHealthDataService, HealthDataService>();
                    services.AddTransient<IHealthAnalysisService, HealthAnalysisService>();
                    services.AddTransient<IHealthDataStorageService, HealthDataStorageService>();
                    services.AddTransient<HealthDataInputApp>();
                    services.AddTransient<HealthDataAnalysisApp>();
                    services.AddSingleton<IHealthMateAppProvider, HealthMateAppProvider>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                });
    }
}
