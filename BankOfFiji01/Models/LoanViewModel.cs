using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankOfFiji01.Models
{
    public class LoanViewModel
    {

        public LoanViewModel()
        {
            MyAccounts=new List<SelectListItem>();
            Assets = new List<SelectListItem>();
            LoanType = new List<SelectListItem>();
        }
        //private BankOfFijiEntities db = new BankOfFijiEntities();

        //int CustID = Convert.ToInt32(HttpContext.Current.Session["CustID"]);
        public int CustID { get; set; }
        public int AccountNo { get; set; }
        public int AssetID { get; set; }
        public int LoanID { get; set; }
        public decimal LoanAmount { get; set; }

        public IList<SelectListItem> MyAccounts { get; set; }
        public IList<SelectListItem> Assets { get; set; }
        public IList<SelectListItem> LoanType { get; set; }

    }
}