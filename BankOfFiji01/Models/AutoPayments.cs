using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOfFiji01.Models
{
    public class AutoPayments
    {
        public int paymentNo { get; set; }
        public int transactionTypeId { get; set; }
        public int sourceAccount { get; set; }
        public int destinationAccount { get; set; }
        public decimal paymentAmount { get; set; }
        public int scheduleId { get; set; }
        public System.DateTime startDate { get; set; }
        public System.DateTime nextDate { get; set; }
        public System.DateTime endDate { get; set; }
        public Nullable<System.DateTime> terminationDate { get; set; }
        public int State_ID { get; set; }

        public virtual BankAccount BankAccount { get; set; }
        public virtual BankAccount BankAccount1 { get; set; }
        public virtual Scheduler Scheduler { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public virtual Automatic_Payment_State Automatic_Payment_State { get; set; }
    }
}