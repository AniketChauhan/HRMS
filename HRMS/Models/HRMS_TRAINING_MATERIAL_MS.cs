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
    
    public partial class HRMS_TRAINING_MATERIAL_MS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRMS_TRAINING_MATERIAL_MS()
        {
            this.HRMS_TRAINING_MATERIALSET = new HashSet<HRMS_TRAINING_MATERIALSET>();
        }
    
        public long Material_ID { get; set; }
        public string Material_Name { get; set; }
        public decimal Material_Rate { get; set; }
        public string Material_Remarks { get; set; }
        public bool Material_IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_TRAINING_MATERIALSET> HRMS_TRAINING_MATERIALSET { get; set; }
    }
}
