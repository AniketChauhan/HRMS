//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HRMS_EMP_CITIZENSHIP_MS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRMS_EMP_CITIZENSHIP_MS()
        {
            this.Employee_Personal_Detail = new HashSet<Employee_Personal_Detail>();
        }
    
        public long CitizenShip_ID { get; set; }
        public string CitizenShip_Country_NM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee_Personal_Detail> Employee_Personal_Detail { get; set; }
    }
}
