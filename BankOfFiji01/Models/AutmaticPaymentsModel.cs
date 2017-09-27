using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOfFiji01.Models
{
    public class AutmaticPaymentsModel
    {
        public AutmaticPaymentsModel()
        {
            AutomaticList = new List<AutoPayments>();
        }
        public IList<AutoPayments> AutomaticList { get; set; }
    }
}