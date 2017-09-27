using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOfFiji01.Models
{
    public class InterestRates
    {
        public int interestRateId { get; set; }
        public string interestRateDetails { get; set; }
        public decimal rateValue { get; set; }
    }
}