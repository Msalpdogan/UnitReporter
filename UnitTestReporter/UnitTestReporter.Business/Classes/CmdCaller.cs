using Microsoft.Extensions.Logging;
using System.Diagnostics;
using UnitTestReporter.Business.Interfaces;

namespace UnitTestReporter.Business.Classes
{
    public class CmdCaller : ICmdCaller
    {
        private readonly ILogger logger;
        public CmdCaller(ILogger<CmdCaller> _logger)
        {
            logger = _logger;
        }
        public string commandCreator()
        {
            logger.LogInformation("Mukiii");
            return "";
        }

        public string runCommand(string exe, string args)
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = exe,
                        Arguments = args,
                        UseShellExecute = true,
                        RedirectStandardOutput = false,
                        RedirectStandardError = false,
                        CreateNoWindow = true
                    }

                };

                process.Start();
                return "";
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex, "commandCreator failed.", null);
                return "";
            }
        }
    }
}
