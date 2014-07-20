using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyABC.Utility.PDFReportGeneration;
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
                LogoPath = "CompanyABCLogo.png",
                Records = FakeData()
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

        private static IEnumerable<Product> FakeData()
        {
            return new List<Product>()
            {
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) },
                new Product() { Title = "Testing 1", Description = "Description 1", Vendor = "Vendor 1", Cost = 10.00M, ListPrice = 15.00M, Status = "Out Of Stock", Location = "Bin 1", DateCreated = DateTime.Now.AddDays(4), DateReceived = DateTime.Now.AddDays(6) }
            };
        }
    }
}
