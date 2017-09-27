using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOfFiji01.Models
{
    public class CustomerProfile
    {
        public System.Guid rowId { get; set; }
        public int customerId { get; set; }
        public int customerTypeId { get; set; }
        public Nullable<System.DateTime> dateOfBirth { get; set; }
        public string postal { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string FNPFNumber { get; set; }
        public string martialStatus { get; set; }
        public string occupation { get; set; }
        public string TIN { get; set; }
        public string passportNumber { get; set; }
        public string residentialType { get; set; }
        public string businessName { get; set; }
        public string sourceOfFunds { get; set; }
        public string employerName { get; set; }
        public Nullable<decimal> salaryPerAnnum { get; set; }
        public byte[] DOBCertificate { get; set; }
        public byte[] FNPFCertificate { get; set; }
        public byte[] TINLetter { get; set; }
        public byte[] businessLicense { get; set; }
        public byte[] passportCpy { get; set; }
        public byte[] photoID { get; set; }

        public virtual CustomerType CustomerType { get; set; }
        public virtual Users Users { get; set; }
    }
}