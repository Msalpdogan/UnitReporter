using SautinSoft.Document;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using UnitTestReporter.Core.Configuration;
using UnitTestReporter.Core.Models;

namespace UnitTestReporter.Business.Reporter
{
    public class ReporterDocx : IReporter<ReporterDocx>
    {
        public ReporterDocx()
        {
        }
        public void CreateReport(Report report)
        {
            string exMessage = "";
            try
            {
                string templatePath = new CommonSettings().DocxTemplate;
                string resultPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\UnitTestReporter\\" + report.FileName + DateTime.Now.ToLongTimeString().Replace(':', '-') + ".docx";
                exMessage = resultPath;
                string isSuccess = "False";
                string tester = System.Environment.MachineName;
                exMessage = exMessage + "\r" + isSuccess;
                exMessage = exMessage + "\r" + tester;

                DocumentCore dc = DocumentCore.Load(templatePath);
                MessageBox.Show(templatePath + "\r///templatePath");
                exMessage = exMessage + "\r" + templatePath;

                if (report.Failed == 0)
                    isSuccess = "True";
                exMessage = exMessage + "\r" + report.Failed;

                var dataSource = new List<object>();

                if (report.TestSuiteList.Count > 0)
                {
                    MessageBox.Show(report.TestSuiteList.Count + "\r///report.TestSuiteList.Count");
                    exMessage = exMessage + "\r" + report.TestSuiteList.Count;


                    foreach (var testSuiteList in report.TestSuiteList)
                    {
                        foreach (var testMethod in testSuiteList.TestList)
                        {
                            dataSource.Add(
                            new
                            {
                                AssemblyName = report.AssemblyName,
                                StartTime = report.StartTime,
                                EndTime = report.EndTime,
                                Total = report.Total,
                                Passed = report.Passed,
                                Failed = report.Failed,
                                Inconclusive = report.Inconclusive,
                                Skipped = report.Skipped,
                                Errors = report.Errors,
                                TestClassName = testSuiteList.Name,
                                TestClassStartTime = testSuiteList.StartTime,
                                TestClassEndTime = testSuiteList.EndTime,
                                TestMethodName = testMethod.Name,
                                TestMethodStartTime = testMethod.StartTime,
                                TestMethodEndTime = testMethod.EndTime,
                                Result = isSuccess,
                                Tester = tester
                            });
                        }
                    }
                }

                dc.MailMerge.Execute(dataSource);
                MessageBox.Show(dataSource + "\r///dc.MailMerge.Execute(dataSource);");

                dc.Save(resultPath);
                MessageBox.Show(resultPath + "\r///dc.Save(resultPath);");
            }
            catch (Exception e)
            {
                Exception ex = new Exception(exMessage+"//"+e.ToString());
                throw ex; 
            }
            


        }
    }
}
