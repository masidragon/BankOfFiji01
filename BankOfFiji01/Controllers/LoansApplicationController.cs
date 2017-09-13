using BankOfFiji01.Models;
using BankOfFiji01.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BankOfFiji01.Controllers
{
    public class LoansApplicationController : Controller
    {

        private System.Object lockThis = new System.Object();
        // GET: LoansApplication
        public async Task<ActionResult> LoanApplication()
        {
            int info = Convert.ToInt32(Session["CustID"]);

            var CheckEligibility = await TransferRepository.CheckBankAccountCount(info);

            if (CheckEligibility <= 1)
            {
                RedirectToAction("TransferInvalid");
            }

            var AccountNumbers = await TransferRepository.CheckBankAccountNumbers(info);
            //var assets = await LoansRepository.GetAssets();
            //var loanlist = await LoansRepository.GetLoanTypes();
            LoanViewModel CreateVM = new LoanViewModel();

            int counter = 0;
            int first = 0;

            foreach (var number in AccountNumbers)
            {
                if (counter == 0)
                {
                    first = number.ID;
                }

                CreateVM.MyAccounts.Add(new SelectListItem()
                {
                    Text = String.Concat(number.ID, " - ", number.Type),
                    Value = number.ID.ToString()
                });
                counter++;
            }

            //foreach (var number in loanlist)
            //{


            //    CreateVM.LoanType.Add(new SelectListItem()
            //    {
            //        Text = String.Concat(number.ID, " - ", number.Type),
            //        Value = number.ID.ToString()
            //    });
            //}

            //foreach (var number in assets)
            //{
            //    if (counter == 0)
            //    {
            //        first = number.ID;
            //    }

            //    CreateVM.Assets.Add(new SelectListItem()
            //    {
            //        Text = String.Concat(number.ID, " - ", number.Type),
            //        Value = number.ID.ToString()
            //    });
            //    counter++;
            //}

            return View(CreateVM);
        }


        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoanApplication(LoanViewModel transactions)
        {
            if (ModelState.IsValid)
            {
    
                    DateTime DateHandler = DateTime.Now;

                    //Check if loan amount is more than minimum
                    if (transactions.LoanAmount < 1000)
                    {
                        TempData["Error"] = "Loan amount is less than minimum loan request";
                        return RedirectToAction("LoanApplication");
                    }

                    //Check if 2 decimal places
                    decimal argument = transactions.LoanAmount;
                    int count = BitConverter.GetBytes(decimal.GetBits(argument)[3])[2];

                    if (count != 2)
                    {
                        TempData["Error"] = "Please enter 2 digits after the decimal place!";
                        return RedirectToAction("LoanApplication");
                    }

                    //Transactions NewTransfer = new Transactions();


                try
                {
                    var content = await LoansRepository.sendloanapplication(transactions);

                    if (content[0] == 'Y')
                    {
                        TempData["Success"] = content;
                        return RedirectToAction("LoanApplication");
                    }

                    TempData["Error"] = content;
                    return RedirectToAction("LoanApplication");
                }
                catch
                {
                    TempData["Error"] = "An error seems to have occured to the return of information from the DB";
                    return RedirectToAction("LoanApplication");
                }

           
        }

            return View(transactions);
        }

    }
}