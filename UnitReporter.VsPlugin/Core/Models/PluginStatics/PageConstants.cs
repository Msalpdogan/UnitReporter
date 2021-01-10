using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitReporter.VsPlugin.Core.Models.PluginStatics
{
    public static class PageConstants
    {
        public static string pageName { get; set; } = "Main";
        public static bool isActiveNextPage { get; set; } = false;
        public static string inputFilePath { get; set; }
        public static string outputFolderPath { get; set; }
        public static string templateFilePath { get; set; }


    }
}
