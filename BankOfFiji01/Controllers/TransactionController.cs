using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankOfFiji01.Models;
using System.Threading.Tasks;
using BankOfFiji01.Repositories;

namespace BankOfFiji01.Controllers
{
    public class TransactionController : Controller
    {
        private BankOfFijiEntities db = new BankOfFijiEntities();

        //private ITransferRepository _repository;


        //public TransactionController() : this(new TransferRepository())
        //{
        //}

        //public TransactionController(ITransferRepository repository)
        //{
        //    _repository = repository;
        //}

        private System.Object lockThis = new System.Object();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.BankAccount).Include(t => t.TransactionType);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return HttpNotFound();
            }
            return View(transactions);
        }

        public ActionResult TransferInvalid()
        {
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

        // GET: Transactions/Create
        //[HttpPost]
        //public async Task<ActionResult> GetAccounts(int info)
        //{
        //    var AccountNumbers = await TransferRepository.CheckBankAccountNumbers(info);
        //    TransactionViewModel CreateVM = new TransactionViewModel();

        //    foreach (var number in AccountNumbers)
        //    {
        //        CreateVM.MyAccountsSelectListItem.Add(new SelectListItem()
        //        {
        //            Text = String.Concat(number.ID, " - ", number.Type),
        //            Value = number.ID.ToString()
        //        });
        //    }

        //    return View(CreateVM);
        //}

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

                    if(content[0] == 'Y')
                    {
                        TempData["Success"] = content;
                        return RedirectToAction("Transfer");
                    }

                    TempData["Error"] = content;
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


        public async Task<ActionResult> TransferAnotherAccount()
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

                    if (content[0] == 'Y')
                    {
                        TempData["Success"] = content;
                        return RedirectToAction("TransferAnotherAccount");
                    }

                    TempData["Error"] = content;
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
        // GET: Transactions/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Transactions transactions = db.Transactions.Find(id);
        //    if (transactions == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.Acc_ID = new SelectList(db.Bank_Account, "Acc_ID", "Acc_ID", transactions.Acc_ID);
        //    ViewBag.Transac_Type_ID = new SelectList(db.Transaction_Type, "Transac_Type_ID", "Transac_Type_Descript", transactions.Transac_Type_ID);
        //    return View(transactions);
        //}

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Trans_ID,Subject_Account,Trans_Date,Trans_Amount,Trans_Adjustment,Acc_ID,Transac_Type_ID")] Transactions transactions)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(transactions).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.Acc_ID = new SelectList(db.Bank_Account, "Acc_ID", "Acc_ID", transactions.Acc_ID);
        //    ViewBag.Transac_Type_ID = new SelectList(db.Transaction_Type, "Transac_Type_ID", "Transac_Type_Descript", transactions.Transac_Type_ID);
        //    return View(transactions);
        //}

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return HttpNotFound();
            }
            return View(transactions);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transactions transactions = db.Transactions.Find(id);
            db.Transactions.Remove(transactions);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
