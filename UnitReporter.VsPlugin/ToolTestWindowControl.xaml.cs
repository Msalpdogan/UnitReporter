using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using UnitTestReporter.Business.Parser;
using UnitTestReporter.Business.Reporter;
using UnitTestReporter.Core.Models;
using UnitTestReporter.Core.Utils;

namespace UnitReporter.VsPlugin
{
    /// <summary>
    /// Interaction logic for ToolTestWindowControl.
    /// </summary>
    public partial class ToolTestWindowControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolTestWindowControl"/> class.
        /// </summary>
        public ToolTestWindowControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Report report= new Report();
            string inputFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\UnitTestReporter\\Test.xml";
            MessageBox.Show(inputFile);
            var reportType = new ParserUtil().GetTestRunnerType(inputFile);
            MessageBox.Show(reportType.ToString());
            if (reportType == UnitTestReporter.Core.Models.TestRunner.NUnit)
            {
               report = new NUnit().Parse(inputFile);
               MessageBox.Show(JsonConvert.SerializeObject(report));
                try
                {
                    new ReporterDocx().CreateReport(report);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "///Button 1 ex");
                    MessageBox.Show(ex.Message + "///Button 1 ex");
                    MessageBox.Show(JsonConvert.SerializeObject(ex) + "///Button 1 ex");
                    MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory );

                    


                    throw;
                }
               MessageBox.Show("Report OK");

            }
            else
            {
                MessageBox.Show("Else");
            }

            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "ToolTestWindow");
        }
    }
}