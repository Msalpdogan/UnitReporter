using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTestReporter.Core.Configuration;
using UnitTestReporter.Core.Models;
using UnitTestReporter.Core.Utils;

namespace UnitTestReporter.Test.Core
{
    [TestFixture]
    public class Parser
    {
        private ILogger<ParserUtil> logger;
        private ILogger<Business.Parser.NUnit> loggerNUnit;

        private IOptions<ParserSettings> options;

        [SetUp]
        public void ParserSetUp()
        {
            logger = Substitute.For<ILogger<ParserUtil>>();
            loggerNUnit = Substitute.For<ILogger<Business.Parser.NUnit>>();

            options = Substitute.For<IOptions<ParserSettings>>();
            options.Value.Returns(new ParserSettings() { JUnitSchemaPath = ".\\TestFiles\\JUnit.xsd" });
        }
        [Test]
        public void getTestRunner_whenNUnitResultFile_shouldReturnNUnit()
        {
            var actual = new ParserUtil(logger,options).GetTestRunnerType(".\\TestFiles\\TestResult.xml");
            Assert.AreEqual(TestRunner.NUnit,actual);
        }

        [Test]
        public void getReport_whenNUnitResultFile_shouldReportIsNotNull()
        {
            var actual = new UnitTestReporter.Business.Parser.NUnit(loggerNUnit).Parse(".\\TestFiles\\TestResult.xml");
            Assert.IsNotNull(actual);
        }
    }
}
