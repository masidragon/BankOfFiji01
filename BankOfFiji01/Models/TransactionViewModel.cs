using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankOfFiji01.Models
{
    public class TransactionViewModel
    {
        public TransactionViewModel()
        {
            MyAccountsSelectListItem = new List<SelectListItem>();
        }

        public IList<SelectListItem> MyAccountsSelectListItem { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int AccountID { get; set; }

        public List<TransactionHistory> StatementDetails { get; set; }

        public string Date { get; set; }
        public string DestinationAccount { get; set; }
        public decimal Amount { get; set; }
        public string Adjustment { get; set; }
    }
}