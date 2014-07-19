using System;
using System.Collections.Generic;

namespace CompanyABC.Utility.PDFReportGeneration
{
    public class PDFGeneratorInfo
    {
        public string LogoPath { get; set; }
        public IEnumerable<string> ColumnRecordNames { get; set; }
        public IEnumerable<object> Records { get; set; }
    }
}
