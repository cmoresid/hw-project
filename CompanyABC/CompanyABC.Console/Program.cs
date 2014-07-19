using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyABC.PDFGeneration.Abstract;
using CompanyABC.PDFGeneration.Entities;
using CompanyABC.PDFGeneration.Templates;
using CompanyABC.Domain.Entities;
using System.IO;

namespace CompanyABC.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            PDFGeneratorInfo info = new PDFGeneratorInfo()
            {
                ColumnRecordNames = new List<string>() { "Column 1" },
                LogoPath = "Logo.png",
                Records = new List<Product>() { new Product() }
            };

            PDFReportGenerator generator = new ProductReportPDFGenerator(info);
            Stream pdfStream = generator.CreatePDFReport();

            using (FileStream fileStream = new FileStream("output.pdf", FileMode.Create, System.IO.FileAccess.Write))
            {
                byte[] bytes = new byte[pdfStream.Length];
                pdfStream.Read(bytes, 0, (int)pdfStream.Length);
                fileStream.Write(bytes, 0, bytes.Length);
                pdfStream.Close();
            }
        }
    }
}
