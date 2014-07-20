using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using CompanyABC.Domain.Repositories;
using CompanyABC.Domain.Search;
using CompanyABC.Utility.PDFReportGeneration;

namespace CompanyABC.WebUI.Controllers
{
    public class PDFReportController : AsyncController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductSearchService _searchService;

        public PDFReportController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
            this._searchService = new ProductSearchService(productRepository);
        }
        
        public void GenerateReportAsync(string search)
        {
            AsyncManager.OutstandingOperations.Increment(1);

            Task.Factory.StartNew(() =>
            {
                PDFReportGenerator productReportGenerator = new ProductReportPDFGenerator(new PDFGeneratorInfo()
                {
                    // TODO: If I have time, allow user to customize what columns they want
                    // to export to PDF.
                    ColumnRecordNames = new List<string>(),
                    LogoPath = Server.MapPath(Url.Content("~/Content/img/logo.png"))
                });

                var products = _productRepository.Products;

                if (!string.IsNullOrEmpty(search))
                    products = _searchService.Search(search);

                return productReportGenerator.CreatePDFReport(products);
            }).ContinueWith(pdfReport => {
                AsyncManager.OutstandingOperations.Decrement();
                AsyncManager.Parameters["pdfFile"] = pdfReport.Result;
            });
        }

        public FileResult GenerateReportCompleted(MemoryStream pdfFile)
        {
            pdfFile.Seek(0, 0);
            return File(pdfFile, "application/pdf", string.Format("export-report-{0}.pdf", DateTime.Now.ToShortDateString()));
        }
    }
}
