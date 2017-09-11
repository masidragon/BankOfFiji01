using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOfFiji01.Repositories
{
    public class UserAccountsDetails
    {
        string UserFullName { get; set; }
        string DOB { get; set; }
        string FNPFNumber { get; set; }
        string ResidentialStatus { get; set; }
        string HomeAddress { get; set; }
        string PostalAddress { get; set; }
    }
}