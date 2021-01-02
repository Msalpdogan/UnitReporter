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
                var info = new ProcessStartInfo(exe);
                info.Arguments = args;
                info.RedirectStandardOutput = true;
                info.WindowStyle = ProcessWindowStyle.Hidden;

                var output = "";
                using (var process = Process.Start(info))
                {
                    output = process.StandardOutput.ReadToEnd();
                }
                return output.Trim();

            }
            catch (System.Exception ex)
            {
                logger.LogError(ex, "commandCreator failed.", null);
                return "";
            }
        }
    }
}
