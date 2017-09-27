using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOfFiji01.Models
{
    public class AssetType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AssetType()
        {
            this.LoanApplications = new HashSet<LoanApplications>();
        }

        public int assetTypeId { get; set; }
        public string assetTypeDesc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoanApplications> LoanApplications { get; set; }
    }
}