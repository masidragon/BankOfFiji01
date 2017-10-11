using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankOfFiji01.Models;
using System.Threading.Tasks;
using BankOfFiji01.Repositories;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System.IO;

namespace BankOfFiji01.Controllers
{
    public class TransactionController : Controller
    {
        private System.Object lockThis = new System.Object();

        public ActionResult TransferInvalid()
        {
            return View();
        }

        public async Task<ActionResult> FIRCA()
        {
            var newpdf = await TransactiontRepository.GetFIRCA();

            MemoryStream memoryStream = new System.IO.MemoryStream();
            Document document = new Document();

            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            var titleFont = FontFactory.GetFont("Arial", 13, Font.BOLD);
            var titleFontBlue = FontFactory.GetFont("Arial", 15, Font.NORMAL, BaseColor.BLUE);
            var boldTableFont = FontFactory.GetFont("Arial", 10, Font.BOLD);
            var bodyFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            var EmailFont = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLUE);
            BaseColor TabelHeaderBackGroundColor = WebColors.GetRGBColor("#EEEEEE");

            PdfPTable headertable = new PdfPTable(3);
            headertable.HorizontalAlignment = 0;
            headertable.WidthPercentage = 100;
            headertable.SetWidths(new float[] { 150f, 220f, 150f });  // then set the column's __relative__ widths
            headertable.DefaultCell.Border = Rectangle.NO_BORDER;

            string imagePath = Server.MapPath("~/Content/img/logo.jpg");
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagePath);
            logo.Alignment = Element.ALIGN_CENTER;

            string aimagePath = Server.MapPath("~/Content/img/2.gif");
            iTextSharp.text.Image alogo = iTextSharp.text.Image.GetInstance(aimagePath);
            alogo.Alignment = Element.ALIGN_CENTER;

            // set width and height
            logo.ScaleToFit(180f, 300f);
            {
                PdfPCell pdfCelllogo = new PdfPCell(logo);
                pdfCelllogo.Border = Rectangle.NO_BORDER;
                pdfCelllogo.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                pdfCelllogo.BorderWidthBottom = 2f;
                headertable.AddCell(pdfCelllogo);
            }

            {
                PdfPCell middlecell = new PdfPCell();
                middlecell.Border = Rectangle.NO_BORDER;
                middlecell.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                middlecell.BorderWidthBottom = 2f;
                headertable.AddCell(middlecell);
            }

            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("Bank of Fiji", titleFont));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase("76 Prince Roads, BOF Building, Suva, Fiji", bodyFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase("(+679) 9992358", bodyFont));
                nextPostCell3.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                PdfPCell nextPostCell4 = new PdfPCell(new Phrase("customercare@bof.com.fj", EmailFont));
                nextPostCell4.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell4);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                nesthousing.BorderWidthBottom = 2f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                headertable.AddCell(nesthousing);
            }

            PdfPTable StatmentHeadertable = new PdfPTable(1);
            StatmentHeadertable.HorizontalAlignment = 0;
            StatmentHeadertable.WidthPercentage = 100;
            StatmentHeadertable.SetWidths(new float[] { 320f });  // then set the column's __relative__ widths
            StatmentHeadertable.DefaultCell.Border = Rectangle.NO_BORDER;

            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("FIRCA STATEMENT", titleFontBlue));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);

                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;

                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                StatmentHeadertable.AddCell(nesthousing);
            }

            document.Add(headertable);
            StatmentHeadertable.PaddingTop = 20f;

            document.Add(StatmentHeadertable);


            PdfPTable table = new PdfPTable(newpdf.Columns.Count);
            BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntColumnHeader = new Font(btnColumnHeader, 10, 1);

            for (int i = 0; i < newpdf.Columns.Count; i++)
            {
                PdfPCell cell = new PdfPCell();
                cell.AddElement(new Chunk(newpdf.Columns[i].ColumnName.ToUpper(), fntColumnHeader));
                table.AddCell(cell);
            }


            for (int i = 0; i < newpdf.Rows.Count; i++)
            {
                for (int j = 0; j < newpdf.Columns.Count; j++)
                {
                    table.AddCell(newpdf.Rows[i][j].ToString());
                }
            }

            // ======== ONLY EDIT CODE *ABOVE* THIS LINE ======== 

            // Adding the data to document
            document.Add(new Phrase("\n"));
            document.Add(table);
            document.Add(new Phrase("\n\n\n"));
            LineSeparator underLine = new LineSeparator(1, 100, null, Element.ALIGN_CENTER, -2);
            document.Add(new Chunk(underLine));
            PdfPTable Infotable = new PdfPTable(1);
            Infotable.HorizontalAlignment = 0;
            Infotable.WidthPercentage = 100;
            Infotable.SetWidths(new float[] { 320f });  // then set the column's __relative__ widths
                                                        // Infotable.DefaultCell.Border = Rectangle.NO_BORDER;

            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("                                         Additional Information", titleFontBlue));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase("\n  1. Interest Income is the money received from the Loan Interest charged to the customers.", bodyFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase("  2. Interest Expenses are the money given to customers in the form of interest that is earned by them on their account(s).", bodyFont));
                nextPostCell3.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                //nesthousing.Border = Rectangle.NO_BORDER;
                //nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                Infotable.AddCell(nesthousing);
            }
            document.Add(Infotable);
            document.Close();

            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();

            Response.Clear();
            Response.ContentType = "application/pdf";

            string filename = DateTime.Now.ToShortDateString() + DateTime.Now.ToShortTimeString();

            Response.AddHeader("Content-Disposition", "attachment; filename=FIRCA_" + filename + ".pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();

            return View();
        }

        public ActionResult NetIncome()
        {
            int info = Convert.ToInt32(Session["CustID"]);
            TransactionViewModel CreateVM = new TransactionViewModel();
            return View(CreateVM);
        }



        [HttpPost]
        public async Task<ActionResult> NetIncome(TransactionViewModel transactions)
        {
            int info = Convert.ToInt32(Session["CustID"]);
            TransactionRequest NewRequest = new TransactionRequest();

            DateTime Start = DateTime.Parse(transactions.StartDate, System.Globalization.CultureInfo.GetCultureInfo("en-us"));
            DateTime End = DateTime.Parse(transactions.EndDate, System.Globalization.CultureInfo.GetCultureInfo("en-us"));

            if (End < Start)
            {
                TempData["Error"] = String.Concat(" Your statement end date is before the start date!");
                return RedirectToAction("BankStatement");
            }

            NewRequest.AccountNumber = transactions.AccountID;
            NewRequest.CustomerID = info;
            NewRequest.EndDate = transactions.EndDate;
            NewRequest.StartDate = transactions.StartDate;
            var newpdf = await TransactiontRepository.GetNetIncome(NewRequest);


            MemoryStream memoryStream = new System.IO.MemoryStream();
            Document document = new Document();

            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            var titleFont = FontFactory.GetFont("Arial", 13, Font.BOLD);
            var titleFontBlue = FontFactory.GetFont("Arial", 15, Font.NORMAL, BaseColor.BLUE);
            var boldTableFont = FontFactory.GetFont("Arial", 10, Font.BOLD);
            var bodyFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            var EmailFont = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLUE);
            BaseColor TabelHeaderBackGroundColor = WebColors.GetRGBColor("#EEEEEE");

            PdfPTable headertable = new PdfPTable(3);
            headertable.HorizontalAlignment = 0;
            headertable.WidthPercentage = 100;
            headertable.SetWidths(new float[] { 150f, 220f, 150f });  // then set the column's __relative__ widths
            headertable.DefaultCell.Border = Rectangle.NO_BORDER;

            string imagePath = Server.MapPath("~/Content/img/logo.jpg");
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagePath);
            logo.Alignment = Element.ALIGN_CENTER;

            // set width and height
            logo.ScaleToFit(180f, 300f);
            {
                PdfPCell pdfCelllogo = new PdfPCell(logo);
                pdfCelllogo.Border = Rectangle.NO_BORDER;
                pdfCelllogo.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                pdfCelllogo.BorderWidthBottom = 2f;
                headertable.AddCell(pdfCelllogo);
            }

            {
                PdfPCell middlecell = new PdfPCell();
                middlecell.Border = Rectangle.NO_BORDER;
                middlecell.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                middlecell.BorderWidthBottom = 2f;
                headertable.AddCell(middlecell);
            }

            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("Bank of Fiji", titleFont));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase("76 Prince Roads, BOF Building, Suva, Fiji", bodyFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase("(+679) 9992358", bodyFont));
                nextPostCell3.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                PdfPCell nextPostCell4 = new PdfPCell(new Phrase("customercare@bof.com.fj", EmailFont));
                nextPostCell4.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell4);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                nesthousing.BorderWidthBottom = 2f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                headertable.AddCell(nesthousing);
            }

            PdfPTable StatmentHeadertable = new PdfPTable(1);
            StatmentHeadertable.HorizontalAlignment = 0;
            StatmentHeadertable.WidthPercentage = 100;
            StatmentHeadertable.SetWidths(new float[] { 320f });  // then set the column's __relative__ widths
            StatmentHeadertable.DefaultCell.Border = Rectangle.NO_BORDER;

            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("NET INCOME STATEMENT", titleFontBlue));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);

                DateTime FirstDay = new DateTime(Start.Year, Start.Month, 1);
                DateTime LastDay = FirstDay.AddMonths(1).AddDays(-1);

                PdfPCell nextPostCell2 = new PdfPCell(new Phrase("\nDate Statement Valid From: " + FirstDay.ToShortDateString(), bodyFont));//please get the date from the user 
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase("Date Statement Valid To: " + LastDay.ToShortDateString(), bodyFont));//please get the date from the user 
                nextPostCell3.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                PdfPCell nextPostCell4 = new PdfPCell(new Phrase("Date Statement Generated: " + DateTime.Now.ToShortDateString(), bodyFont));
                nextPostCell4.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell4);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                //nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                StatmentHeadertable.AddCell(nesthousing);
            }

            document.Add(headertable);
            StatmentHeadertable.PaddingTop = 20f;

            document.Add(StatmentHeadertable);
            //LineSeparator underLine = new LineSeparator(1, 100, null, Element.ALIGN_CENTER, -2);
            //document.Add(new Chunk(underLine));

            //PdfContentByte cb = writer.DirectContent;
            //var rect = new iTextSharp.text.Rectangle(500, 0, 50, 50);
            //rect.Border = iTextSharp.text.Rectangle.BOX;
            //rect.BorderWidth = 5;
            //rect.BorderColor = new BaseColor(0, 0, 0);
            //cb.Rectangle(rect);

            PdfPTable table = new PdfPTable(newpdf.Columns.Count);
            BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntColumnHeader = new Font(btnColumnHeader, 10, 1);
            for (int i = 0; i < newpdf.Columns.Count; i++)
            {
                PdfPCell cell = new PdfPCell();
                cell.AddElement(new Chunk(newpdf.Columns[i].ColumnName.ToUpper(), fntColumnHeader));
                table.AddCell(cell);
            }


            for (int i = 0; i < newpdf.Rows.Count; i++)
            {
                for (int j = 0; j < newpdf.Columns.Count; j++)
                {
                    table.AddCell(newpdf.Rows[i][j].ToString());
                }
            }

            // ======== ONLY EDIT CODE *ABOVE* THIS LINE ======== 

            // Adding the data to document
            document.Add(new Phrase("\n"));
            document.Add(table);
            document.Add(new Phrase("\n\n\n"));
            LineSeparator underLine = new LineSeparator(1, 100, null, Element.ALIGN_CENTER, -2);
            document.Add(new Chunk(underLine));
            PdfPTable Infotable = new PdfPTable(1);
            Infotable.HorizontalAlignment = 0;
            Infotable.WidthPercentage = 100;
            Infotable.SetWidths(new float[] { 320f });  // then set the column's __relative__ widths
                                                        // Infotable.DefaultCell.Border = Rectangle.NO_BORDER;

            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("                                         Additional Information", titleFontBlue));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase("\n  1. Interest Income is the money received from the Loan Interest charged to the customers.", bodyFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase("  2. Interest Expenses are the money given to customers in the form of interest that is earned by them on their account(s).", bodyFont));
                nextPostCell3.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                //nesthousing.Border = Rectangle.NO_BORDER;
                //nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                Infotable.AddCell(nesthousing);
            }
            document.Add(Infotable);
            document.Close();

            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();

            Response.Clear();
            Response.ContentType = "application/pdf";

            string filename = DateTime.Now.ToShortDateString() + DateTime.Now.ToShortTimeString();

            Response.AddHeader("Content-Disposition", "attachment; filename=BankOfFiji_NetIncome_" + filename + ".pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();

            return View();
        }

        public async Task<ActionResult> BankStatement()
        {
            int info = Convert.ToInt32(Session["CustID"]);
            var AccountNumbers = await TransferRepository.CheckBankAccountNumbers(info);
            TransactionViewModel CreateVM = new TransactionViewModel();

            foreach (var number in AccountNumbers)
            {
                CreateVM.MyAccountsSelectListItem.Add(new SelectListItem()
                {
                    Text = String.Concat(number.Type, " - ", number.ID),
                    Value = number.ID.ToString()
                });
            }

            return View(CreateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BankStatement(TransactionViewModel transactions)
        {
            int info = Convert.ToInt32(Session["CustID"]);
            TransactionRequest NewRequest = new TransactionRequest();

            DateTime Start = DateTime.Parse(transactions.StartDate, System.Globalization.CultureInfo.GetCultureInfo("en-us"));
            DateTime End = DateTime.Parse(transactions.EndDate, System.Globalization.CultureInfo.GetCultureInfo("en-us"));

            if (End < Start)
            {
                TempData["Error"] = String.Concat(" Your statement end date is before the start date!");
                return RedirectToAction("BankStatement");
            }

            NewRequest.AccountNumber = transactions.AccountID;
            NewRequest.CustomerID = info;
            NewRequest.EndDate = transactions.EndDate;
            NewRequest.StartDate = transactions.StartDate;
            var Statement = await TransactiontRepository.GetStatement(NewRequest);

            var AccountNumbers = await TransferRepository.CheckBankAccountNumbers(info);

            TransactionViewModel CreateVM = new TransactionViewModel();

            foreach (var number in AccountNumbers)
            {
                CreateVM.MyAccountsSelectListItem.Add(new SelectListItem()
                {
                    Text = String.Concat(number.Type, " - ", number.ID),
                    Value = number.ID.ToString()
                });
            }
            CreateVM.StatementDetails = Statement;

            return View(CreateVM);

            //transactions.StatementDetails = Statement;

            //return View();
        }

        public async Task<ActionResult> Transfer()
        {
            int info = Convert.ToInt32(Session["CustID"]);

            var CheckEligibility = await TransferRepository.CheckBankAccountCount(info);

            if (CheckEligibility <= 1)
            {
                RedirectToAction("TransferInvalid");
            }

            var AccountNumbers = await TransferRepository.CheckBankAccountNumbers(info);

            TransferViewModel CreateVM = new TransferViewModel();

            int counter = 0;
            int first = 0;

            foreach (var number in AccountNumbers)
            {
                if (counter == 0)
                {
                    first = number.ID;
                }

                CreateVM.MyAccountsSelectListItem.Add(new SelectListItem()
                {
                    Text = String.Concat(number.ID, " - ", number.Type),
                    Value = number.ID.ToString()
                });
                counter++;
            }

            foreach (var number in AccountNumbers)
            {
                if (number.ID != first)
                {
                    CreateVM.MyOtherAccounts.Add(new SelectListItem()
                    {
                        Text = String.Concat(number.ID, " - ", number.Type),
                        Value = number.ID.ToString()
                    });
                }
            }

            return View(CreateVM);
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Transfer(TransferViewModel transactions)
        {
            if (ModelState.IsValid)
            {
                lock (lockThis)
                {
                    DateTime DateHandler = DateTime.Now;

                    //Check if seelcted second account
                    if (transactions.TransferAcc_ID == 0)
                    {
                        TempData["Error"] = "You did not select an account to transfer to!";
                        return RedirectToAction("Transfer");
                    }

                    // Check if minimum transfer amount met
                    if (transactions.Trans_Amount < 10)
                    {
                        TempData["Error"] = "You did not meet the minimum transfer amount!";
                        return RedirectToAction("Transfer");
                    }

                    //Check if 2 decimal places
                    decimal argument = transactions.Trans_Amount;
                    int count = BitConverter.GetBytes(decimal.GetBits(argument)[3])[2];

                    if (count != 2)
                    {
                        TempData["Error"] = "Please enter 2 digits after the decimal place!";
                        return RedirectToAction("Transfer");
                    }

                    Transactions NewTransfer = new Transactions();

                }

                // ID for Transfers
                transactions.Transac_Type_ID = 3;
                try
                {
                    var content = await TransferRepository.EnableTransfer(transactions);

                    if (content.TransferStatus[0] == 'Y')
                    {
                        TempData["Success"] = content.TransferStatus;
                        return RedirectToAction("Transfer");
                    }

                    TempData["Error"] = content.TransferStatus;
                    return RedirectToAction("Transfer");
                }
                catch
                {
                    TempData["Error"] = "An error seems to have occured to the return of information from the DB";
                    return RedirectToAction("Transfer");
                }

            }
            return View(transactions);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> GetMyOtherAccounts()
        {
            int Acc_ID = Convert.ToInt32(Request.QueryString["Acc_ID"]);

            if (String.IsNullOrEmpty(Request.QueryString["Acc_ID"]))
            {
                throw new ArgumentNullException("Acc_ID");
            }

            var content = await TransferRepository.GetOtherAccounts(Acc_ID);

            if (content == null)
            {
                var nullvalues = (from r in content
                                  select new
                                  {
                                      id = 0,
                                      name = String.Concat("You cannot use this feature because you have one registered account")
                                  }).ToList();

                return Json(nullvalues, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = (from r in content
                              select new
                              {
                                  id = r.ID.ToString(),
                                  name = String.Concat(r.ID, " - ", r.Type)
                              }).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }



        public async Task<ActionResult> AutoTransfer()
        {
            int info = Convert.ToInt32(Session["CustID"]);

            var CheckEligibility = await TransferRepository.CheckBankAccountCount(info);

            if (CheckEligibility <= 1)
            {
                RedirectToAction("TransferInvalid");
            }

            var AccountNumbers = await TransferRepository.CheckBankAccountNumbers(info);
            var Timeperiod = await BillPaymentRepository.GetTimeperiod();

            TransferViewModel CreateVM = new TransferViewModel();

            int counter = 0;
            int first = 0;

            foreach (var number in AccountNumbers)
            {
                if (counter == 0)
                {
                    first = number.ID;
                }

                CreateVM.MyAccountsSelectListItem.Add(new SelectListItem()
                {
                    Text = String.Concat(number.ID, " - ", number.Type),
                    Value = number.ID.ToString()
                });
                counter++;
            }

            foreach (var number in AccountNumbers)
            {
                if (number.ID != first)
                {
                    CreateVM.MyOtherAccounts.Add(new SelectListItem()
                    {
                        Text = String.Concat(number.ID, " - ", number.Type),
                        Value = number.ID.ToString()
                    });
                }
            }

            foreach (var item in Timeperiod)
            {
                CreateVM.TransferPeriod.Add(new SelectListItem()
                {
                    Text = String.Concat(item.IntervalDescription),
                    Value = item.IntervalID.ToString()
                });
            }

            return View(CreateVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AutoTransfer(TransferViewModel transactions)
        {
            if (ModelState.IsValid)
            {
                lock (lockThis)
                {
                    DateTime DateHandler = DateTime.Now;

                    //Check if seelcted second account
                    if (transactions.TransferAcc_ID == 0)
                    {
                        TempData["Error"] = "You did not select an account to transfer to!";
                        return RedirectToAction("AutoTransfer");
                    }

                    if (transactions.Period == 0)
                    {
                        TempData["Error"] = "You did not select an payment interval!";
                        return RedirectToAction("AutoTransfer");
                    }

                    // Check if minimum transfer amount met
                    if (transactions.Trans_Amount < 10)
                    {
                        TempData["Error"] = "You did not meet the minimum transfer amount!";
                        return RedirectToAction("AutoTransfer");
                    }


                    if (transactions.startDate < DateTime.Now)
                    {
                        TempData["Error"] = "You did not Specify a correct start date";
                        return RedirectToAction("AutoTransfer");
                    }

                    if (transactions.endDate < DateTime.Now)
                    {
                        TempData["Error"] = "You did not Specify a correct end date";
                        return RedirectToAction("AutoTransfer");
                    }

                    if (transactions.endDate < transactions.startDate)
                    {
                        TempData["Error"] = "Please Check your dates";
                        return RedirectToAction("AutoTransfer");
                    }
                    //Check if 2 decimal places
                    decimal argument = transactions.Trans_Amount;
                    int count = BitConverter.GetBytes(decimal.GetBits(argument)[3])[2];

                    if (count != 2)
                    {
                        TempData["Error"] = "Please enter 2 digits after the decimal place!";
                        return RedirectToAction("AutoTransfer");
                    }



                    Transactions NewTransfer = new Transactions();

                }

                // ID for Transfers
                transactions.Transac_Type_ID = 3;
                try
                {
                    var content = await TransferRepository.EnableTransfer(transactions);

                    if (content.TransferStatus[0] == 'Y')
                    {
                        TempData["Success"] = content.TransferStatus;
                        return RedirectToAction("AutoTransfer");
                    }

                    TempData["Error"] = content.TransferStatus;
                    return RedirectToAction("AutoTransfer");
                }
                catch
                {
                    TempData["Error"] = "An error seems to have occured to the return of information from the DB";
                    return RedirectToAction("AutoTransfer");
                }

            }
            return View(transactions);
        }
        
        public async Task<ActionResult> TransferAnotherAccount()
        {
            int info = Convert.ToInt32(Session["CustID"]);

            var CheckEligibility = await TransferRepository.CheckBankAccountCount(info);

            if (CheckEligibility <= 1)
            {
                RedirectToAction("TransferInvalid");
            }

            var AccountNumbers = await TransferRepository.CheckBankAccountNumbers(info);
            var Timeperiod = await BillPaymentRepository.GetTimeperiod();
            TransferViewModel CreateVM = new TransferViewModel();

            int counter = 0;
            int first = 0;

            foreach (var number in AccountNumbers)
            {
                if (counter == 0)
                {
                    first = number.ID;
                }

                CreateVM.MyAccountsSelectListItem.Add(new SelectListItem()
                {
                    Text = String.Concat(number.ID, " - ", number.Type),
                    Value = number.ID.ToString()
                });
                counter++;
            }

            foreach (var number in AccountNumbers)
            {
                if (number.ID != first)
                {
                    CreateVM.MyOtherAccounts.Add(new SelectListItem()
                    {
                        Text = String.Concat(number.ID, " - ", number.Type),
                        Value = number.ID.ToString()
                    });
                }
            }

            foreach (var item in Timeperiod)
            {
                CreateVM.TransferPeriod.Add(new SelectListItem()
                {
                    Text = String.Concat(item.IntervalDescription),
                    Value = item.IntervalID.ToString()
                });
            }
            return View(CreateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TransferAnotherAccount(TransferViewModel transactions)
        {
            if (ModelState.IsValid)
            {
                lock (lockThis)
                {
                    DateTime DateHandler = DateTime.Now;

                    //Check if seelcted second account
                    if (transactions.TransferAcc_ID == 0)
                    {
                        TempData["Error"] = "You did not select an account to transfer to!";
                        return RedirectToAction("TransferAnotherAccount");
                    }

                    // Check if minimum transfer amount met
                    if (transactions.Trans_Amount < 10)
                    {
                        TempData["Error"] = "You did not meet the minimum transfer amount!";
                        return RedirectToAction("TransferAnotherAccount");
                    }

                    //Check if 2 decimal places
                    decimal argument = transactions.Trans_Amount;
                    int count = BitConverter.GetBytes(decimal.GetBits(argument)[3])[2];

                    if (count != 2)
                    {
                        TempData["Error"] = "Please enter 2 digits after the decimal place!";
                        return RedirectToAction("TransferAnotherAccount");
                    }

                    Transactions NewTransfer = new Transactions();

                }

                // ID for Transfers
                transactions.Transac_Type_ID = 3;
                try
                {
                    var content = await TransferRepository.EnableTransfer(transactions);

                    if (content.TransferStatus[0] == 'Y')
                    {
                        TempData["Success"] = content.TransferStatus;
                        return RedirectToAction("TransferAnotherAccount");
                    }

                    TempData["Error"] = content.TransferStatus;
                    return RedirectToAction("TransferAnotherAccount");
                }
                catch
                {
                    TempData["Error"] = "An error seems to have occured to the return of information from the DB";
                    return RedirectToAction("TransferAnotherAccount");
                }

            }
            return View(transactions);
        }

        public async Task<ActionResult> AutoTransferAnotherAccount()
        {
            int info = Convert.ToInt32(Session["CustID"]);

            var CheckEligibility = await TransferRepository.CheckBankAccountCount(info);

            if (CheckEligibility <= 1)
            {
                RedirectToAction("TransferInvalid");
            }

            var AccountNumbers = await TransferRepository.CheckBankAccountNumbers(info);
            var Timeperiod = await BillPaymentRepository.GetTimeperiod();

            TransferViewModel CreateVM = new TransferViewModel();

            int counter = 0;
            int first = 0;

            foreach (var number in AccountNumbers)
            {
                if (counter == 0)
                {
                    first = number.ID;
                }

                CreateVM.MyAccountsSelectListItem.Add(new SelectListItem()
                {
                    Text = String.Concat(number.ID, " - ", number.Type),
                    Value = number.ID.ToString()
                });
                counter++;
            }

            foreach (var number in AccountNumbers)
            {
                if (number.ID != first)
                {
                    CreateVM.MyOtherAccounts.Add(new SelectListItem()
                    {
                        Text = String.Concat(number.ID, " - ", number.Type),
                        Value = number.ID.ToString()
                    });
                }
            }
            foreach (var item in Timeperiod)
            {
                CreateVM.TransferPeriod.Add(new SelectListItem()
                {
                    Text = String.Concat(item.IntervalDescription),
                    Value = item.IntervalID.ToString()
                });
            }

            return View(CreateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AutoTransferAnotherAccount(TransferViewModel transactions)
        {
            if (ModelState.IsValid)
            {
                lock (lockThis)
                {
                    DateTime DateHandler = DateTime.Now;

                    //Check if seelcted second account
                    if (transactions.TransferAcc_ID == 0)
                    {
                        TempData["Error"] = "You did not select an account to transfer to!";
                        return RedirectToAction("AutoTransferAnotherAccount");
                    }

                    // Check if minimum transfer amount met
                    if (transactions.Trans_Amount < 10)
                    {
                        TempData["Error"] = "You did not meet the minimum transfer amount!";
                        return RedirectToAction("AutoTransferAnotherAccount");
                    }


                    if (transactions.startDate < DateTime.Now)
                    {
                        TempData["Error"] = "You did not Specify a correct start date";
                        return RedirectToAction("AutoTransferAnotherAccount");
                    }

                    if (transactions.endDate < DateTime.Now)
                    {
                        TempData["Error"] = "You did not Specify a correct end date";
                        return RedirectToAction("AutoTransferAnotherAccount");
                    }

                    if (transactions.endDate < transactions.startDate)
                    {
                        TempData["Error"] = "Please Check your dates";
                        return RedirectToAction("AutoTransferAnotherAccount");
                    }
                    //Check if 2 decimal places
                    decimal argument = transactions.Trans_Amount;
                    int count = BitConverter.GetBytes(decimal.GetBits(argument)[3])[2];

                    if (count != 2)
                    {
                        TempData["Error"] = "Please enter 2 digits after the decimal place!";
                        return RedirectToAction("AutoTransferAnotherAccount");
                    }



                    Transactions NewTransfer = new Transactions();

                }

                // ID for Transfers
                //transactions.Transac_Type_ID = 3;
                try
                {
                    transactions.Transac_Type_ID = 3;

                    var content = await TransferRepository.EnableTransfer(transactions);

                    if (content.TransferStatus[0] == 'Y')
                    {
                        TempData["Success"] = content.TransferStatus;
                        return RedirectToAction("AutoTransferAnotherAccount");
                    }

                    TempData["Error"] = content.TransferStatus;
                    return RedirectToAction("AutoTransferAnotherAccount");
                }
                catch
                {
                    TempData["Error"] = "An error seems to have occured to the return of information from the DB";
                    return RedirectToAction("AutoTransferAnotherAccount");
                }

            }
            return View(transactions);
        }

    }
}
