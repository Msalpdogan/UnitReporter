﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestReporter.Core.Configuration
{
    public class ParserSettings
    {
        public string JUnitSchemaPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\UnitTestReporter\\JUnit.xsd";
    }
}
