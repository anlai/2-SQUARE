using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace _2SQUARE.Controllers
{
    public class ReportController : ApplicationController
    {
        private readonly Font _titleFont = new Font(Font.FontFamily.TIMES_ROMAN, 24, Font.BOLD);
        private readonly Font _sectionFont = new Font(Font.FontFamily.TIMES_ROMAN, 18, Font.BOLD);
        private readonly Font _font = new Font(Font.FontFamily.TIMES_ROMAN, 12);
        private readonly Font _labelFont = new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD);

        /// <summary>
        /// Generates the final report
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FileResult GenerateReport(int id)
        {
            var project = Db.Projects.Where(a => a.Id == id).FirstOrDefault();

            var document = new iTextSharp.text.Document(PageSize.LETTER, 36 /* left */, 36 /* right */, 62 /* top */, 52 /* bottom */);
            // set the variable for the page's actual content size
            var pageWidth = document.PageSize.Width - (document.LeftMargin + document.RightMargin);
            var pageHeight = document.PageSize.Height - (document.TopMargin + document.BottomMargin);

            var ms = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, ms);

            document.Open();

            // using a table for formatting
            var table = new PdfPTable(2);
            table.TotalWidth = pageWidth;
            table.SetWidths(new int[] {(int)Math.Floor(pageWidth*.2), (int)Math.Floor(pageWidth*.80)});

            // add the project information
            table.AddCell(CreateCell(new Paragraph(project.Name, _titleFont), true));

            table.AddCell(CreateCell(new Paragraph("Description", _labelFont)));
            table.AddCell(CreateCell(new Paragraph(string.IsNullOrEmpty(project.Description) ? "No description available." : project.Description, _font)));

            table.AddCell(CreateCell(new Paragraph("Security", _titleFont), true));

            table.AddCell(CreateCell(new Paragraph("Terms", _labelFont)));


            table.AddCell(CreateCell(new Paragraph("Privacy", _titleFont), true));

            document.Add(table);

            document.Close();

            var bytes = ms.ToArray();
            return File(bytes, "application/pdf", "project.pdf");
        }

        private PdfPCell CreateCell(Paragraph paragraph, bool span = false)
        {
            var cell = new PdfPCell(paragraph);
            if (span) cell.Colspan = 2;

            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;

            cell.PaddingTop = 16f;
            cell.PaddingBottom = 10f;

            return cell;
        }
    }
}
