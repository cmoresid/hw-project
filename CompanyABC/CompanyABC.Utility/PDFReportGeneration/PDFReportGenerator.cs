using System;
using System.IO;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;


namespace CompanyABC.Utility.PDFReportGeneration
{
    public abstract class PDFReportGenerator
    {
        protected Document _document;
        protected Table _table;
        protected PDFGeneratorInfo _info;
        protected int _pageNumber;

        public PDFReportGenerator(PDFGeneratorInfo info)
        {
            this._info = info;
            this._pageNumber = 0;
        }

        public System.IO.Stream CreatePDFReport()
        {
            this._document = new Document();

            DefineStyles();
            BuildReport();

            return CreateMemoryStreamFromDocument();
        }

        private MemoryStream CreateMemoryStreamFromDocument()
        {
            MemoryStream reportMemoryStream = new MemoryStream();
            PdfDocumentRenderer reportRenderer = new PdfDocumentRenderer(false, PdfSharp.Pdf.PdfFontEmbedding.Default);
            reportRenderer.Document = this._document;
            reportRenderer.RenderDocument();
            reportRenderer.Save(reportMemoryStream, false);
            reportMemoryStream.Position = 0;
            return reportMemoryStream;
        }

        private void DefineStyles()
        {
            this._document.DefaultPageSetup.Orientation = Orientation.Landscape;

            // Get the predefined style Normal.
            Style style = this._document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Verdana";

            style = this._document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = this._document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Create a new style called Table based on style Normal
            style = this._document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Verdana";
            style.Font.Name = "Times New Roman";
            style.Font.Size = 9;

            // Create a new style called Reference based on style Normal
            style = this._document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
        }

        protected void CreatePage()
        {
            // Each MigraDoc document needs at least one section.
            Section currentSection = this._document.LastSection;

            if (currentSection == null)
            {
                currentSection = this._document.AddSection();
            }
            else
            {
                currentSection.AddPageBreak();
            }

            this._pageNumber += 1;

            // Put a logo in the header
            Image image = currentSection.Headers.Primary.AddImage(this._info.LogoPath);
            image.Height = "2.5cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Left;
            image.WrapFormat.Style = WrapStyle.Through;

            // Create footer
            Paragraph paragraph = currentSection.Footers.Primary.AddParagraph();
            paragraph.AddText(CompanyABC.Utility.PDFReportGeneration.Constants.CompanyInfo.COMPANY_FOOTER);
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // Add the print date field
            paragraph = currentSection.AddParagraph();
            paragraph.Format.SpaceBefore = "2cm";
            paragraph.Style = "Reference";
            paragraph.AddFormattedText("EXPORTED RESULTS", TextFormat.Bold);
            paragraph.AddTab();
            paragraph.AddTab();
            paragraph.AddTab();
            paragraph.AddTab();
            paragraph.AddText("Date Generated: ");
            paragraph.AddDateField("MM-dd-yyyy");

            // Create the item table
            this._table = currentSection.AddTable();
            this._table.Style = "Table";
            this._table.Borders.Color = CompanyABC.Utility.PDFReportGeneration.Constants.ColorScheme.TableBorder;
            this._table.Borders.Width = 0.25;
            this._table.Borders.Left.Width = 0.5;
            this._table.Borders.Right.Width = 0.5;
            this._table.Rows.LeftIndent = 0;
        }

        protected abstract void BuildReport();
    }
}
