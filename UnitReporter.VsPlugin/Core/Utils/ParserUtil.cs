﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using UnitTestReporter.Core.Configuration;
using UnitTestReporter.Core.Models;

namespace UnitTestReporter.Core.Utils
{
    public class ParserUtil
    {

        private readonly ParserSettings _parserSettings;

        public ParserUtil()
        {
            _parserSettings = new ParserSettings();
        }


        public TestRunner GetTestRunnerType(string _filePath)
        {
            var doc = new XmlDocument();

            try
            {
                doc.Load(_filePath);

                if (doc.DocumentElement == null)
                    return TestRunner.Unknown;

                var fileExtension = Path.GetExtension(_filePath).ToLower();

                XmlNamespaceManager nsmgr;
                if (fileExtension.EndsWith("trx"))
                {
                    // MSTest2010
                    nsmgr = new XmlNamespaceManager(doc.NameTable);
                    nsmgr.AddNamespace("ns", "http://microsoft.com/schemas/VisualStudio/TeamTest/2010");

                    // check if its a mstest 2010 xml file 
                    // will need to check the "//TestRun/@xmlns" attribute - value = http://microsoft.com/schemas/VisualStudio/TeamTest/2010
                    var testRunNode = doc.SelectSingleNode("ns:TestRun", nsmgr);
                    if (testRunNode != null && testRunNode.Attributes != null && testRunNode.Attributes["xmlns"] != null &&
                        testRunNode.Attributes["xmlns"].InnerText.Contains("2010"))
                    {
                        //return TestRunner.MSTest2010;
                        return TestRunner.Unknown;
                    }
                }

                if (fileExtension.EndsWith("xml"))
                {
                    // Gallio
                    nsmgr = new XmlNamespaceManager(doc.NameTable);
                    nsmgr.AddNamespace("ns", "http://www.gallio.org/");

                    var model = doc.SelectSingleNode("//ns:testModel", nsmgr);
                    if (model != null) 
                        return TestRunner.Unknown;
                        //return TestRunner.Gallio;


                    // xUnit - will have <assembly ... test-framework="xUnit.net 2....."/>
                    var assemblyNode = doc.SelectSingleNode("//assembly");
                    if (assemblyNode != null && assemblyNode.Attributes != null &&
                        assemblyNode.Attributes["test-framework"] != null)
                    {
                        var testFramework = assemblyNode.Attributes["test-framework"].InnerText.ToLower();

                        if (testFramework.Contains("xunit"))
                        {
                            return TestRunner.Unknown;

                            //if (testFramework.Contains(" 2."))
                            //{
                            //    return TestRunner.XUnitV2;
                            //}
                            //else if (testFramework.Contains(" 1."))
                            //{
                            //    return TestRunner.XUnitV1;
                            //}
                        }
                    }

                    if (ValidateJUnitXsd(doc, _filePath))
                    {
                        return TestRunner.JUnit;
                    }

                    // NUnit
                    // NOTE: not all nunit test files (ie when have nunit output format from other test runners) will contain the environment node
                    //            but if it does exist - then it should have the nunit-version attribute
                    var envNode = doc.SelectSingleNode("//environment");
                    if (envNode != null && envNode.Attributes != null && envNode.Attributes["nunit-version"] != null)
                        return TestRunner.NUnit;

                    // check for test-suite nodes - if it has those - its probably nunit tests
                    var testSuiteNodes = doc.SelectNodes("//test-suite");
                    if (testSuiteNodes != null && testSuiteNodes.Count > 0) return TestRunner.NUnit;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("Error when trying to determine testrunner for file {0}: {1}", _filePath, ex.Message));
            }

            return TestRunner.Unknown;
        }

        private bool ValidateJUnitXsd(XmlDocument doc, string _filePath, bool _validJunitSchema = true)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream(_parserSettings.JUnitSchemaPath))
                using (var reader = new StreamReader(stream))
                {
                    var schema = new XmlSchemaSet();

                    schema.Add("", XmlReader.Create(reader));

                    doc.Schemas.Add(schema);
                    doc.Schemas.Compile();
                    doc.Validate((s, o) =>
                    {
                        _validJunitSchema = false;
                    });

                    return _validJunitSchema;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("Error when trying to determine testrunner for file {0}: {1}", _filePath, ex.Message));
                return false;
            }
        }
    }
}
