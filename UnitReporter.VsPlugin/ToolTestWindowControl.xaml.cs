using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using UnitReporter.VsPlugin.Core.Models.PluginStatics;
using UnitTestReporter.Business.Parser;
using UnitTestReporter.Business.Reporter;
using UnitTestReporter.Core.Models;
using UnitTestReporter.Core.Utils;

namespace UnitReporter.VsPlugin
{
    /// <summary>
    /// Interaction logic for ToolTestWindowControl.
    /// </summary>
    public partial class ToolTestWindowControl : System.Windows.Controls.UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolTestWindowControl"/> class.
        /// </summary>
        public ToolTestWindowControl()
        {
            this.InitializeComponent();
           
            progressBar.Value = 0;
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void Report_Click(object sender, RoutedEventArgs e)
        {
            Report report = new Report();
            string inputFile = PageConstants.inputFilePath;
            var reportType = new ParserUtil().GetTestRunnerType(inputFile);
            progressBar.Value += 10;

            if (reportType == UnitTestReporter.Core.Models.TestRunner.NUnit)
            {
                report = new NUnit().Parse(inputFile);
                progressBar.Value += 20;

                try
                {
                    new ReporterDocx().CreateReport(report,PageConstants.outputFolderPath);
                    progressBar.Value += 20;

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("EX MUH \r"+ex.ToString());

                }
                System.Windows.MessageBox.Show("Report OK");

            }
            else
            {
                System.Windows.MessageBox.Show("Else");
            }
        }

        private void OutputSelect_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description ="Select the directory that you want to use as the default.";
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.Personal;
            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                OutputLbl.Content = folderBrowserDialog.SelectedPath;
                PageConstants.outputFolderPath = folderBrowserDialog.SelectedPath;
                progressBar.Value += 18;
                ready4Report();
            }
        }

        private void SelectInputFileButton_Click(object sender, RoutedEventArgs e)
        {
             OpenFileDialog openFileDialog = new OpenFileDialog();
             openFileDialog.Multiselect = false;
             openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
             if (openFileDialog.ShowDialog() == DialogResult.OK)
             {
                InputLbl.Content = openFileDialog.FileName;
                PageConstants.inputFilePath = openFileDialog.FileName;
                progressBar.Value += 18;
                ready4Report();
            }
        }
        private void ready4Report()
        {
            if (string.IsNullOrEmpty(PageConstants.inputFilePath) || string.IsNullOrEmpty(PageConstants.outputFolderPath) || string.IsNullOrEmpty(PageConstants.templateFilePath))
                button1.IsEnabled = false;
            else
            {
                button1.IsEnabled = true;
                button1.Content = "Create Report";
            }
        }

        private void Template_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Word Documents|*.docx";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                TemplateLbl.Content = openFileDialog.FileName;
                PageConstants.templateFilePath = openFileDialog.FileName;
                progressBar.Value += 14;
                ready4Report();
            }
        }


        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.Multiselect = false;
        //    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //         lbFiles.Items.Add(Path.GetFileName(openFileDialog.FileName));
        //    }
        //}
    }
}