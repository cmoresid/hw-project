using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyABC.PDFGeneration.Entities
{
    public class PDFGeneratorInfo
    {
        public string LogoPath { get; set; }
        public IEnumerable<string> ColumnRecordNames { get; set; }
        public IEnumerable<object> Records { get; set; }
    }
}
