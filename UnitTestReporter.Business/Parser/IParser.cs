using System;
using System.Collections.Generic;
using System.Text;
using UnitTestReporter.Core.Models;

namespace UnitTestReporter.Business.Parser
{
    public interface IParser<T> where T : class
    {
        Report Parse(string filePath);
    }
}
