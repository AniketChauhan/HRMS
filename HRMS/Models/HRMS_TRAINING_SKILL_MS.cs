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
    
    public partial class HRMS_TRAINING_SKILL_MS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRMS_TRAINING_SKILL_MS()
        {
            this.HRMS_Training_Request_Application = new HashSet<HRMS_Training_Request_Application>();
        }
    
        public long Skill_ID { get; set; }
        public string Skill_Name { get; set; }
        public string Skill_Remark { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_Training_Request_Application> HRMS_Training_Request_Application { get; set; }
    }
}