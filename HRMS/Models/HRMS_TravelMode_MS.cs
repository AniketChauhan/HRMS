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
    
    public partial class HRMS_TravelMode_MS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRMS_TravelMode_MS()
        {
            this.HRMS_TravelGroupType_MS = new HashSet<HRMS_TravelGroupType_MS>();
        }
    
        public long Mode_ID { get; set; }
        public string Mode_Type { get; set; }
        public string Mode_Name { get; set; }
        public string Mode_Short_Name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_TravelGroupType_MS> HRMS_TravelGroupType_MS { get; set; }
    }
}
