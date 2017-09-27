using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOfFiji01.Models
{
    public class AccountApplications
    {
        public int accountApplicationNo { get; set; }
        public int userId { get; set; }
        public int accountTypeId { get; set; }
        public decimal openingBalance { get; set; }
        public string verifiedAndOpenedBy { get; set; }
        public string authorizedBy { get; set; }
        public Nullable<System.DateTime> approvedDate { get; set; }
        public int applicationStatusId { get; set; }

        public virtual AccountType AccountType { get; set; }
        public virtual ApplicationStatus ApplicationStatus { get; set; }
        public virtual Users Users { get; set; }
    }
}