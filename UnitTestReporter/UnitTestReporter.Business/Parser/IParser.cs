using System;
using System.Collections.Generic;
using System.Text;
using UnitTestReporter.Core.Models;

namespace UnitTestReporter.Business.Parser
{
    public interface IParser
    {
        Report Parse(string filePath);
    }
}
