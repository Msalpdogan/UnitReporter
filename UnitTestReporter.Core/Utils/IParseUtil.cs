using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestReporter.Core.Models;

namespace UnitTestReporter.Core.Utils
{
    public interface IParseUtil
    {
        public TestRunner GetTestRunnerType(string _filePath);

    }
}
