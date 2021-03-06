﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOfFiji01.Models
{
    public class BankAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BankAccount()
        {
            this.AutoPayments = new HashSet<AutoPayments>();
            this.AutoPayments1 = new HashSet<AutoPayments>();
            this.LoanApplications = new HashSet<LoanApplications>();
            this.Transactions = new HashSet<Transactions>();
            this.Transactions1 = new HashSet<Transactions>();
        }

        public int accountNo { get; set; }
        public int userId { get; set; }
        public int accountTypeId { get; set; }
        public System.DateTime startDate { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }
        public decimal debitBal { get; set; }
        public decimal creditBal { get; set; }
        public decimal totalInterest { get; set; }
        public int taxId { get; set; }
        public int accountStatusId { get; set; }
        public Nullable<System.DateTime> interestdate { get; set; }

        public virtual AccountStatus AccountStatus { get; set; }
        public virtual AccountType AccountType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AutoPayments> AutoPayments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AutoPayments> AutoPayments1 { get; set; }
        public virtual TaxRates TaxRates { get; set; }
        public virtual Users Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoanApplications> LoanApplications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transactions> Transactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transactions> Transactions1 { get; set; }
    }
}