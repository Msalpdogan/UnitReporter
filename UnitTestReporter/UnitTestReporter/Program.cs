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
using UnitTestReporter.Business.Parser;
using UnitTestReporter.Business.Reporter;

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
            var featureConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(".\\Files\\featuresettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();


            ///Generate Host Builder and Register the Services for DI
            var builder = new HostBuilder()
                   .ConfigureServices((hostContext, services) =>
                   {
                       services.AddScoped<BaseForm>();
                       services.AddOptions();

                       services.AddSingleton<ICmdCaller, CmdCaller>();
                       services.AddSingleton<IParser<NUnit>, NUnit>();
                       services.AddSingleton<IParser<JUnit>, JUnit>();
                       services.AddSingleton<IReporter<ReporterDocx>, ReporterDocx>();



                       //Add Serilog
                       LogLevel minLogLevel = (LogLevel)Int32.Parse(configuration.GetSection("Common").GetSection("MinLogLevel").Value);
                       string template = configuration.GetSection("Common").GetSection("OutputTemplate").Value;

                       var serilogLogger = new LoggerConfiguration()
                                   .WriteTo.File(".\\Logs\\Reporter.log", rollingInterval: RollingInterval.Hour, outputTemplate: template)
                                   .CreateLogger();
                       services.AddLogging(x =>
                       {
                           x.SetMinimumLevel(minLogLevel);
                           x.AddSerilog(logger: serilogLogger, dispose: true);
                       });

                       //Add Config
                       services.Configure<CommonSettings>(configuration.GetSection("Common"));
                       services.Configure<ParserSettings>(configuration.GetSection("Parser"));
                       services.Configure<FeatureReportSettings>(featureConfiguration.GetSection("FeatureReporter"));

                   });
            

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var form1 = services.GetRequiredService<BaseForm>();

                    Application.Run(form1);
                    
                    Console.WriteLine("Success");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Occured" + ex.Message);
                }
            }
        }
    }
}
