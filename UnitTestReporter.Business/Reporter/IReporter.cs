using System;
using System.Collections.Generic;
using System.Text;
using UnitTestReporter.Core.Models;

namespace UnitTestReporter.Business.Reporter
{
    public interface IReporter<T> where T: class
    {
        void CreateReport(Report report, string templatePath, string outputFolder);
    }
}
