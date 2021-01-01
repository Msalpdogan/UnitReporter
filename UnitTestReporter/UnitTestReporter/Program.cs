using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using Microsoft.Extensions.Options;
using System.Windows.Forms;
using UnitTestReporter.Business.Classes;
using UnitTestReporter.Business.Interfaces;
using UnitTestReporter.Core.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace UnitTestReporter
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ///Configuration 
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(".\\Files\\appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();


            ///Generate Host Builder and Register the Services for DI
            var builder = new HostBuilder()
                   .ConfigureServices((hostContext, services) =>
                   {
                       services.AddScoped<Form1>();
                       services.AddOptions();

                       services.AddSingleton<ICmdCaller, CmdCaller>();

                       //Add Serilog
                       var serilogLogger = new LoggerConfiguration()
                                   .WriteTo.File(".\\Logs\\Reporter.log", rollingInterval: RollingInterval.Hour)
                                   .CreateLogger();
                       services.AddLogging(x =>
                       {
                           x.SetMinimumLevel(LogLevel.Trace);
                           x.AddSerilog(logger: serilogLogger, dispose: true);
                       });

                       //Add Config
                       services.Configure<AppSettings>(configuration.GetSection("App"));

                   });


            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var form1 = services.GetRequiredService<Form1>();
                    Application.Run(form1);

                    Console.WriteLine("Success");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Occured");
                }
            }
        }
    }
}
