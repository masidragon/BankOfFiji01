using BankOfFiji01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BankOfFiji01.Controllers
{
    public class BillPaymentsController : Controller
    {
        private System.Object lockThis = new System.Object();
        // GET: BillPayments
        public async Task<ActionResult> BillPayment()
        {
            int info = Convert.ToInt32(Session["CustID"]);

            var CheckEligibility = await TransferRepository.CheckBankAccountCount(info);

            if (CheckEligibility <= 1)
            {
                RedirectToAction("TransferInvalid");
            }

            var AccountNumbers = await TransferRepository.CheckBankAccountNumbers(info);
            var Companies = await BillPaymentRepository.GetCompanyAccounts();

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

            foreach(var item in Companies)
            {
                CreateVM.CompanyAccounts.Add(new SelectListItem()
                {
                    Text=String.Concat(item.ID, " - ", item.AccountName),
                    Value = item.ID.ToString()
                });
            }
            return View(CreateVM);
        }

   
        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BillPayment(TransferViewModel transactions)
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
                        return RedirectToAction("BillPayment");
                    }

                    // Check if minimum transfer amount met
                    if (transactions.Trans_Amount < 10)
                    {
                        TempData["Error"] = "You did not meet the minimum transfer amount!";
                        return RedirectToAction("BillPayment");
                    }

                    //Check if 2 decimal places
                    decimal argument = transactions.Trans_Amount;
                    int count = BitConverter.GetBytes(decimal.GetBits(argument)[3])[2];

                    if (count != 2)
                    {
                        TempData["Error"] = "Please enter 2 digits after the decimal place!";
                        return RedirectToAction("BillPayment");
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
                        return RedirectToAction("BillPayment");
                    }

                    TempData["Error"] = content;
                    return RedirectToAction("BillPayment");
                }
                catch
                {
                    TempData["Error"] = "An error seems to have occured to the return of information from the DB";
                    return RedirectToAction("BillPayment");
                }

            }
            return View(transactions);
        }
    }
}