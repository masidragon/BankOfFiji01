using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankOfFiji01.Models
{
    public class TransferViewModel
    {
        public TransferViewModel()
        {
            MyAccountsSelectListItem = new List<SelectListItem>();
            MyOtherAccounts = new List<SelectListItem>();
            CompanyAccounts = new List<SelectListItem>();
            TransferPeriod = new List<SelectListItem>();

        }

        //private BankOfFijiEntities db = new BankOfFijiEntities();

        int CustIDHandler = Convert.ToInt32(HttpContext.Current.Session["CustID"]);

        public IList<SelectListItem> MyAccountsSelectListItem { get; set; }

        public IList<SelectListItem> MyOtherAccounts { get; set; }
        public IList<SelectListItem> CompanyAccounts { get; set; }
        public IList<SelectListItem> TransferPeriod { get; set; }

        public int TransferAcc_ID { get; set; }
        public int Transac_Type_ID { get; set; }
        public decimal Trans_Amount { get; set; }
        public int Acc_ID { get; set; }

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int Period { get; set; }
    }
}