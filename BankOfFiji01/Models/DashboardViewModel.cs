using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOfFiji01.Models
{
    public class DashboardViewModel
    {
        public List<BankAccount> UserBankAccounts { get; set; }

        public string UserName { get; set; }
    }
}