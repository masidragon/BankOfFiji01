using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOfFiji01.Models
{
    public class Loan
    {
           public int CustID = Convert.ToInt32(HttpContext.Current.Session["CustID"]);
        public int AccountNo { get; set; }
            public int AssetID { get; set; }
            public int LoanID { get; set; }
            public decimal LoanAmount { get; set; }
    }
}