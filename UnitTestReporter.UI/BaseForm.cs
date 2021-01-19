using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Windows.Forms;
using UnitTestReporter.Business.Interfaces;
using UnitTestReporter.Business.Parser;
using UnitTestReporter.Business.Reporter;
using UnitTestReporter.Core.Configuration;
using UnitTestReporter.Core.Models;
using UnitTestReporter.Core.Utils;

namespace UnitTestReporter
{
    public partial class BaseForm : Form
    {
        private readonly ILogger logger;
        private readonly ICmdCaller cmdCaller;
        private readonly CommonSettings options;
        private readonly IParser<NUnit> nunitParser;
        private readonly IParser<JUnit> junitParser;
        private readonly IReporter<ReporterDocx> reporter;
        private readonly IParseUtil parserUtil;
        private Report report;
        private string inputFilePath;
        private string templateFilePath;
        private string outputFolderPath;


        public BaseForm(ILogger<BaseForm> _logger, ICmdCaller _cmdCaller, IOptions<CommonSettings> _options, IParser<NUnit> _nunit, IParser<JUnit> _junit, IReporter<ReporterDocx> _reporter, IParseUtil _parseUtil)
        {
            logger = _logger;
            cmdCaller = _cmdCaller;
            options = _options.Value;
            nunitParser = _nunit;
            junitParser = _junit;
            reporter = _reporter;
            parserUtil = _parseUtil;
            report = new Report();

            

            InitializeComponent();
            //writeSystemStatus();
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

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                inputFilePath = openFileDialog.FileName;
                inputFile.Text = inputFilePath;
                progressBarValueChanger(10);
                logger.LogInformation("Input file : " + inputFilePath);
                ready4Report();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Word Documents|*.docx";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                templateFilePath = openFileDialog.FileName;
                templateFile.Text= templateFilePath;
                progressBarValueChanger(10);
                logger.LogInformation("Template file : " + templateFilePath);
                ready4Report();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select the directory that you want to use as the default.";
            folderBrowserDialog.ShowNewFolderButton = true;
            
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                outputFolderPath = folderBrowserDialog.SelectedPath;
                outputFolder.Text = outputFolderPath;
                progressBarValueChanger(10);
                logger.LogInformation("Output file : " + outputFolderPath);
                ready4Report();
            }
        }
        private void ready4Report()
        {
            if (string.IsNullOrEmpty(outputFolderPath) || string.IsNullOrEmpty(inputFilePath) || string.IsNullOrEmpty(templateFilePath)  )
            {
                reportButton.Enabled = false;
                logger.LogInformation("Reporter is not ready ");

            }
            else
            {
                reportButton.Enabled = true;
                reportButton.Text = "Create Report";
                logger.LogInformation("Reporter is ready ");
            }
        }
        private void progressBarValueChanger(int value)
        {
            
            if (progressBar.Maximum < (progressBar.Value + value))
            {
                progressBar.Value = progressBar.Maximum;
            }
            else
            {
                progressBar.Value += value;
            }
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            var type = parserUtil.GetTestRunnerType(inputFilePath);
            progressBarValueChanger(10);

            bool isSuccess = true;

            if (type == Core.Models.TestRunner.NUnit)
            {
                report = nunitParser.Parse(inputFilePath);
                progressBarValueChanger(10);

            }
            else if (type == Core.Models.TestRunner.JUnit)
            {
                report = junitParser.Parse(inputFilePath);
                progressBarValueChanger(10);

            }
            else
            {
                progressBar.ForeColor = System.Drawing.Color.Red;
                progressBarValueChanger(20);
                isSuccess = false;
                inputFilePath = string.Empty;
                inputFile.Text = string.Empty;
                logger.LogError("File Format unknown");
                ready4Report();

            }

            if (isSuccess)
            {
                reporter.CreateReport(report, templateFilePath, outputFolderPath);
                progressBarValueChanger(40);
            }
        }
    }
}
