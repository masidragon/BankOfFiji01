using BankOfFiji01.Repositories;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BankOfFiji01.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public async Task<ActionResult> Home()
        {
            int CustIDHandler = Convert.ToInt32(Session["CustID"]);
            var UserDetail = await DashboardRepo.FindUserDetails(CustIDHandler);

            return View(UserDetail);
        }

        public async Task<ActionResult> pdf_Click(object sender, EventArgs e)
        {
            var newpdf = await DashboardRepo.getpdf();

            MemoryStream memoryStream = new System.IO.MemoryStream();
            Document document = new Document();

            PdfPTable table = new PdfPTable(newpdf.Columns.Count);
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

            document.Open();

            BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntColumnHeader = new Font(btnColumnHeader, 10, 1);
            for (int i = 0; i < newpdf.Columns.Count; i++)
            {
                PdfPCell cell = new PdfPCell();
                cell.AddElement(new Chunk(newpdf.Columns[i].ColumnName.ToUpper(), fntColumnHeader));
                table.AddCell(cell);
            }
            //table Data
            for (int i = 0; i < newpdf.Rows.Count; i++)
            {
                for (int j = 0; j < newpdf.Columns.Count; j++)
                {
                    table.AddCell(newpdf.Rows[i][j].ToString());
                }
            }

            string filename = DateTime.Now.Date.ToShortDateString();

            document.Add(table);
            document.Close();

            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();

            Response.Clear();
            Response.ContentType = "application/pdf";
            
            Response.AddHeader("Content-Disposition", "attachment; filename=BankOfFiji_NetIncome_"+filename+".pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();

            return View();
        }
    }
}