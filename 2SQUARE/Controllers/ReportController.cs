using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Resources;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace _2SQUARE.Controllers
{
    public class ReportController : ApplicationController
    {
        private readonly Font _titleFont = new Font(Font.FontFamily.TIMES_ROMAN, 24, Font.BOLD);
        private readonly Font _font = new Font(Font.FontFamily.TIMES_ROMAN, 12);
        private readonly Font _bold = new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD);
        private readonly Font _italic = new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.ITALIC);

        /// <summary>
        /// Generates the final report
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FileResult GenerateReport(int id)
        {
            var project = Db.Projects.Include("ProjectTerms").Include("Goals").Include("Goals.GoalType")
                .Include("SecurityElicitationType").Include("SecurityElicitationType")
                .Include("Requirements").Include("Requirements.Category").Include("Requirements.SquareType")
                .Where(a => a.Id == id).FirstOrDefault();

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
            table.LockedWidth = true;
            table.SplitLate = false;
            table.SetWidths(new int[] {(int)Math.Floor(pageWidth*.2), (int)Math.Floor(pageWidth*.80)});

            // add the project information
            table.AddCell(CreateCell(new Paragraph(project.Name, _titleFont), true));

            table.AddCell(CreateCell(new Paragraph("Description", _bold)));
            table.AddCell(CreateCell(new Paragraph(string.IsNullOrEmpty(project.Description) ? "No description available." : project.Description, _font)));

            // security section
            var security = Db.SquareTypes.Where(a => a.Name == SquareTypes.Security).Single();
            table.AddCell(CreateCell(new Paragraph("Security", _titleFont), true));

            // security terms
            table.AddCell(CreateCell(new Paragraph("Terms", _bold)));
            table.AddCell(CreateTerms(project, security));

            // security goals
            table.AddCell(CreateCell(new Paragraph("Business Goal", _bold)));
            table.AddCell(CreateGoals(project, GoalTypes.Business));
            table.AddCell(CreateCell(new Paragraph("Security Goals", _bold)));
            table.AddCell(CreateGoals(project, GoalTypes.Security));

            // elicitation type and justification
            table.AddCell(CreateCell(new Paragraph("Elicitation Technique", _bold)));
            table.AddCell(CreateCell(new Paragraph(project.SecurityElicitationType != null ? project.SecurityElicitationType.Name : "Security Elicitation Technique not selected.", _font)));
            table.AddCell(CreateCell(new Paragraph("Rationale", _bold)));
            table.AddCell(CreateCell(new Paragraph(project.SecurityElicitationRationale, _font)));

            // requirements
            table.AddCell(CreateCell(new Paragraph("Requirements", _bold), true));
            table.AddCell(CreateRequirements(project, security, pageWidth));

            // privacy section
            table.AddCell(CreateCell(new Paragraph("Privacy", _titleFont), true));

            document.Add(table);

            document.Close();

            var bytes = ms.ToArray();
            return File(bytes, "application/pdf", "project.pdf");
        }

        private PdfPCell CreateCell(Paragraph paragraph, bool span = false)
        {
            var cell = new PdfPCell();

            if (paragraph != null) cell.AddElement(paragraph);
            if (span) cell.Colspan = 2;

            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;

            cell.PaddingTop = 16f;
            cell.PaddingBottom = 10f;

            return cell;
        }

        private PdfPCell CreateTerms(Project project, SquareType squareType)
        {
            var cell = CreateCell(null);

            if (project.ProjectTerms.Count == 0) cell.AddElement(new Paragraph("No Terms Defined.", _font));

            foreach (var term in project.ProjectTerms.Where(a=>a.SquareType == squareType).OrderBy(a=>a.Term))
            {
                var paragraph = new Paragraph();
                paragraph.SpacingAfter = 7f;

                paragraph.Add(new Phrase(term.Term + " - ", _bold));
                paragraph.Add(new Phrase(term.Definition, _font));
                paragraph.Add(new Phrase(string.Format(" [{0}]", term.Source), _italic));

                cell.AddElement(paragraph);
            }

            return cell;
        }

        private PdfPCell CreateGoals(Project project, string goalTypeId)
        {
            var cell = CreateCell(null);

            var goals = project.Goals.Where(a => a.GoalType.Id == goalTypeId).ToList();

            if (goals.Count == 0) cell.AddElement(new Paragraph("No Goal Defined", _font));
            else if (goals.Count == 1)
            {
                var paragraph = new Paragraph(goals.First().Description, _font);
                cell.AddElement(paragraph);
            }
            else
            {
                var list = new List(false);

                foreach (var goal in goals)
                {
                    var paragraph = new Paragraph();

                    paragraph.Add(new Paragraph(goal.Name, _bold));
                    paragraph.Add(new Paragraph(goal.Description, _font));

                    list.Add(new ListItem(paragraph));
                }     
           
                cell.AddElement(list);
            }

            return cell;
        }

        private PdfPCell CreateRequirements(Project project, SquareType squareType, float pageWidth)
        {
            var cell = CreateCell(null, true);

            var table = new PdfPTable(5);
            table.TotalWidth = pageWidth;
            table.LockedWidth = true;
            table.SetWidths(new int[] { 50, (int)Math.Floor(pageWidth - 300), 100, 75, 75 });

            // add the headers
            table.AddCell(new PdfPCell(new Phrase("Id", _bold)));
            table.AddCell(new PdfPCell(new Phrase("Requirement", _bold)));
            table.AddCell(new PdfPCell(new Phrase("Category", _bold)));
            table.AddCell(new PdfPCell(new Phrase("Priority", _bold)));
            table.AddCell(new PdfPCell(new Phrase("Essential", _bold)));

            // add the rows
            foreach (var req in project.Requirements.Where(a=>a.SquareType.Id == squareType.Id).OrderBy(a => a.Order).ThenBy(a => a.Priority))
            {
                table.AddCell(new PdfPCell(new Phrase(req.RequirementId, _font)));
                table.AddCell(new PdfPCell(new Phrase(req.RequirementText, _font)));
                table.AddCell(new PdfPCell(new Phrase(req.Category.Name, _font)));
                table.AddCell(new PdfPCell(new Phrase(req.Priority.ToString(), _font)));
                table.AddCell(new PdfPCell(new Phrase(req.Essential ? "x" : string.Empty, _font)));
            }

            cell.AddElement(table);

            return cell;
        }
    }
}
