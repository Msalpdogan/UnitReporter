using Microsoft.Extensions.Options;
using SautinSoft.Document;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTestReporter.Core.Configuration;
using UnitTestReporter.Core.Models;

namespace UnitTestReporter.Business.Reporter
{
    public class ReporterDocx : IReporter<ReporterDocx>
    {
        private readonly CommonSettings commonSettings;
        public ReporterDocx(IOptions<CommonSettings> options)
        {
            commonSettings = options.Value;
        }
        public void CreateReport(Report report, string outputPath)
        {
            string templatePath = commonSettings.DocxTemplate;
            string resultPath = report.FileName + DateTime.Now.ToShortDateString() + ".docx";
            string isSuccess = "False";
            string tester = System.Environment.MachineName;

            DocumentCore dc = DocumentCore.Load(templatePath);

            if (report.Failed == 0)
                isSuccess = "True";

            if (report.TestSuiteList.Count > 0)
            {
                foreach (var testSuiteList in report.TestSuiteList)
                {
                    foreach (var testMethod in testSuiteList.TestList)
                    {
                        var dataSource = new[] {
                        new {
                            AssemblyName = report.AssemblyName,
                            StartTime = report.StartTime,
                            EndTime = report.EndTime,
                            Total= report.Total,
                            Passed=report.Passed,
                            Failed= report.Failed,
                            Inconclusive= report.Inconclusive,
                            Skipped = report.Skipped,
                            Errors= report.Errors,
                            TestClassName = testSuiteList.Name,
                            TestClassStartTime = testSuiteList.StartTime,
                            TestClassEndTime = testSuiteList.EndTime,
                            TestMethodName =testMethod.Name,
                            TestMethodStartTime = testMethod.StartTime,
                            TestMethodEndTime = testMethod.EndTime,
                            Result = isSuccess,
                            Tester = tester
                        } };
                        dc.MailMerge.Execute(dataSource);
                    }
                }
            }

            dc.Save(resultPath);

            // Open the result for demonstration purposes.
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(resultPath) { UseShellExecute = true });
        }
    }
}
