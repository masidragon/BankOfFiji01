﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOfFiji01.Models
{
    public class LoanRecords
    {
        public int loanApplicationNo { get; set; }
        public System.DateTime repaymtStartDate { get; set; }
        public System.DateTime repaymtEndDate { get; set; }
        public int LoanStatusId { get; set; }

        public virtual LoanApplications LoanApplications { get; set; }
        public virtual LoanStatus LoanStatus { get; set; }
    }
}