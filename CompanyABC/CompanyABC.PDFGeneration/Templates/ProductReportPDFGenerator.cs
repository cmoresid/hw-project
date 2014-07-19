using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyABC.PDFGeneration.Abstract;
using CompanyABC.PDFGeneration.Entities;
using CompanyABC.Domain.Entities;

namespace CompanyABC.PDFGeneration.Templates
{
    public sealed class ProductReportPDFGenerator : PDFReportGenerator
    {
        public ProductReportPDFGenerator(PDFGeneratorInfo info)
            : base(info)
        {
        }

        protected override void PopulateTable()
        {
            IEnumerable<Product> products = _info.Records.Cast<Product>();
        }
    }
}
