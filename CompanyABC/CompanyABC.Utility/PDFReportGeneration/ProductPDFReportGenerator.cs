using System;
using System.Collections.Generic;
using System.Linq;
using CompanyABC.Domain.Entities;

namespace CompanyABC.Utility.PDFReportGeneration
{
    public sealed class ProductReportPDFGenerator : PDFReportGenerator
    {
        public ProductReportPDFGenerator(PDFGeneratorInfo info)
            : base(info)
        {
        }

        protected override void PopulateTable()
        {
            var products = _info.Records.Cast<Product>();
        }
    }
}
