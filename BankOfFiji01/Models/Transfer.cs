using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOfFiji01.Models
{
    public class Transfer
    {
        public int TransferAcc_ID { get; set; }
        public int Transac_Type_ID { get; set; }
        public decimal Trans_Amount { get; set; }
        public int Acc_ID { get; set; }
    }
}