using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOfFiji01.Models
{
    public class LoanType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoanType()
        {
            this.LoanApplications = new HashSet<LoanApplications>();
        }

        public int LoanTypeId { get; set; }
        public string LoanTypeDesc { get; set; }
        public decimal LoanInterestRate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoanApplications> LoanApplications { get; set; }
    }
}