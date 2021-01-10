using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestReporter.Core.Configuration
{
    public class CommonSettings
    {
        //Trace = 0,
        //Debug = 1,
        //Information = 2,
        //Warning = 3,
        //Error = 4,
        //Critical = 5,
        //None = 6
        public int MinLogLevel { get; set; } = 0;
        public int StatisticSenderFlag { get; set; } = 0; //not use yet.
        public string OutputTemplate { get; set; } = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";
        public static string DocxTemplate { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\UnitTestReporter\\template.docx";

    }
}
