using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOfFiji01.Models
{
    public class Scheduler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Scheduler()
        {
            this.AutoPayments = new HashSet<AutoPayments>();
        }

        public int scheduleId { get; set; }
        public string schedule { get; set; }
        public int scheduleperiod { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AutoPayments> AutoPayments { get; set; }
    }
}