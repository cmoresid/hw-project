using System;
using System.Collections.Generic;
using System.Linq;
using CompanyABC.Domain.Entities;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using PagedList;

namespace CompanyABC.Utility.PDFReportGeneration
{
    public sealed class ProductReportPDFGenerator : PDFReportGenerator
    {
        public ProductReportPDFGenerator(PDFGeneratorInfo info)
            : base(info)
        {
        }

        protected override void BuildReport()
        {
            var products = _info.Records.Cast<Product>();

            int pageNumber = 1;
            var pagedProducts = products.OrderBy(product => product.ABCID).ToPagedList<Product>(pageNumber, 30);
            int numberOfPages = pagedProducts.PageCount;

            while (pageNumber <= numberOfPages)
            {
                base.CreatePage();

                // Title
                Column column = this._table.AddColumn("2.5cm");
                column.Format.Alignment = ParagraphAlignment.Left;

                // Description
                column = this._table.AddColumn("5.0cm");
                column.Format.Alignment = ParagraphAlignment.Left;

                // Vendor
                column = this._table.AddColumn("2.0cm");
                column.Format.Alignment = ParagraphAlignment.Left;

                // Cost
                column = this._table.AddColumn("1.5cm");
                column.Format.Alignment = ParagraphAlignment.Right;

                // List Price
                column = this._table.AddColumn("2cm");
                column.Format.Alignment = ParagraphAlignment.Right;

                // Status
                column = this._table.AddColumn("2cm");
                column.Format.Alignment = ParagraphAlignment.Center;

                // Location
                column = this._table.AddColumn("3cm");
                column.Format.Alignment = ParagraphAlignment.Left;

                // Date Created
                column = this._table.AddColumn("3cm");
                column.Format.Alignment = ParagraphAlignment.Right;

                // Date Received
                column = this._table.AddColumn("3cm");
                column.Format.Alignment = ParagraphAlignment.Right;

                // Create the header of the table
                Row row = this._table.AddRow();
                row.HeadingFormat = true;
                row.Format.Alignment = ParagraphAlignment.Center;
                row.Format.Font.Bold = true;
                row.Shading.Color = CompanyABC.Utility.PDFReportGeneration.Constants.ColorScheme.TableBlue;

                row.Cells[0].AddParagraph("Title");
                row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;

                row.Cells[1].AddParagraph("Description");
                row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Bottom;

                row.Cells[2].AddParagraph("Vendor");
                row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[2].VerticalAlignment = VerticalAlignment.Bottom;

                row.Cells[3].AddParagraph("Cost");
                row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[3].VerticalAlignment = VerticalAlignment.Bottom;

                row.Cells[4].AddParagraph("List Price");
                row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[4].VerticalAlignment = VerticalAlignment.Bottom;

                row.Cells[5].AddParagraph("Status");
                row.Cells[5].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[5].VerticalAlignment = VerticalAlignment.Bottom;

                row.Cells[6].AddParagraph("Location");
                row.Cells[6].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[6].VerticalAlignment = VerticalAlignment.Bottom;

                row.Cells[7].AddParagraph("Date Created");
                row.Cells[7].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[7].VerticalAlignment = VerticalAlignment.Bottom;

                row.Cells[8].AddParagraph("Date Received");
                row.Cells[8].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[8].VerticalAlignment = VerticalAlignment.Bottom;

                this._table.SetEdge(0, 0, 9, 1, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);
                int count = 0;

                foreach (var product in pagedProducts)
                {
                    Row productRow = this._table.AddRow();

                    productRow.Shading.Color = (count % 2 == 0)
                        ? CompanyABC.Utility.PDFReportGeneration.Constants.ColorScheme.TableWhite
                        : CompanyABC.Utility.PDFReportGeneration.Constants.ColorScheme.TableGray;

                    productRow.Cells[0].AddParagraph(product.Title);
                    productRow.Cells[1].AddParagraph(product.Description != null ? product.Description : "---");
                    productRow.Cells[2].AddParagraph(product.Vendor);
                    productRow.Cells[3].AddParagraph(product.Cost.ToString("c"));
                    productRow.Cells[4].AddParagraph(product.ListPrice.ToString("c"));
                    productRow.Cells[5].AddParagraph(product.Status);
                    productRow.Cells[6].AddParagraph(product.Location);
                    productRow.Cells[7].AddParagraph(product.DateCreated.ToString("MM-dd-yyyy"));
                    productRow.Cells[8].AddParagraph(product.DateReceived.HasValue ? product.DateReceived.GetValueOrDefault().ToString("MM-dd-yyyy") : "---");

                    count++;
                }

                pageNumber++;
                pagedProducts = products.OrderBy(product => product.ABCID).ToPagedList<Product>(pageNumber, 30);

                Paragraph pageNumberParagraph = this._document.LastSection.AddParagraph("pg. " + this._pageNumber);
                pageNumberParagraph.Format.Alignment = ParagraphAlignment.Center;
                pageNumberParagraph.Format.SpaceBefore = "1.0cm";
            }


        }
    }
}
