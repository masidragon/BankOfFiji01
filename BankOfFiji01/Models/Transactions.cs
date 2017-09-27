using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOfFiji01.Models
{
    public class Transactions
    {
        public System.Guid transcId { get; set; }
        public int transactionTypeId { get; set; }
        public System.DateTime transcDate { get; set; }
        public int sourceAccount { get; set; }
        public int destinationAccount { get; set; }
        public decimal transcAmount { get; set; }

        public virtual BankAccount BankAccount { get; set; }
        public virtual BankAccount BankAccount1 { get; set; }
        public virtual TransactionType TransactionType { get; set; }
    }
}