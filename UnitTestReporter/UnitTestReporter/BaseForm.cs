using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Windows.Forms;
using UnitTestReporter.Business.Interfaces;
using UnitTestReporter.Core.Configuration;

namespace UnitTestReporter
{
    public partial class BaseForm : Form
    {
        private readonly ILogger logger;
        private readonly ICmdCaller cmdCaller;
        private readonly CommonSettings options;

        public BaseForm(ILogger<BaseForm> _logger, ICmdCaller _cmdCaller, IOptions<CommonSettings> _options)
        {
            logger = _logger;
            cmdCaller = _cmdCaller;
            options = _options.Value;
            InitializeComponent();
            writeSystemStatus();
            writeConfiguration();
        }




        private void writeSystemStatus()
        {
            try
            {
                var memorielines = cmdCaller.runCommand("wmic", "OS get FreePhysicalMemory,TotalVisibleMemorySize /Value").Split('\n');
                var freeMemory = memorielines[0].Split("=", StringSplitOptions.RemoveEmptyEntries)[1].Replace("\r", null);
                var totalMemory = memorielines[1].Split("=", StringSplitOptions.RemoveEmptyEntries)[1];

                logger.LogInformation($"Free Memory : {freeMemory} ** Total Memory : {totalMemory}");

                var cpuLines = cmdCaller.runCommand("wmic", "CPU get Name,LoadPercentage /Value").Split('\n');
                var CpuUse = cpuLines[0].Split("=", StringSplitOptions.RemoveEmptyEntries)[1].Replace("\r", null);
                var CpuName = cpuLines[1].Split("=", StringSplitOptions.RemoveEmptyEntries)[1];

                logger.LogInformation($"CpuUse : {CpuUse} ** CpuName : {CpuName}");

                var whoami = cmdCaller.runCommand("whoami", "");

                logger.LogInformation($"User : {whoami} ");
            }
            catch (Exception ex)
            {
                logger.LogError($"writeSystemStatus ex : \r {ex.Message} ");
            }

        }
        private void writeConfiguration()
        {
            try
            {
                var propertyInfos = options.GetType().GetProperties();

                var sb = new StringBuilder();

                foreach (var info in propertyInfos)
                {
                    var value = info.GetValue(options, null) ?? "(null)";
                    sb.AppendLine($"\t {info.Name} : {value.ToString()}");
                }
                logger.LogInformation($"Common Options : \r {sb.ToString()} ");
            }
            catch (Exception ex)
            {
                logger.LogError($"writeConfiguration ex : \r {ex.Message} ");
            }
        }
    }
}
