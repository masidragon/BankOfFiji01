using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOfFiji01.Models
{
    public class LoanApplications
    {
        public int loanApplicationNo { get; set; }
        public int accountNo { get; set; }
        public int assetTypeId { get; set; }
        public decimal monthlyRent_MortageAmt { get; set; }
        public decimal desiredLoanAmt { get; set; }
        public int loanTypeId { get; set; }
        public string approver { get; set; }
        public Nullable<System.DateTime> approvedDate { get; set; }
        public int applicationStatusId { get; set; }

        public virtual ApplicationStatus ApplicationStatus { get; set; }
        public virtual AssetType AssetType { get; set; }
        public virtual BankAccount BankAccount { get; set; }
        public virtual LoanType LoanType { get; set; }
        public virtual LoanRecords LoanRecords { get; set; }
    }
}