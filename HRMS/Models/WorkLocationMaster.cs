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

    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class WorkLocationMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkLocationMaster()
        {
            this.HRMS_Emp_Details = new HashSet<HRMS_Emp_Details>();
        }
        public long WorkID { get; set; }
        [Required]
        [DisplayName("Work Location Name")]
        [MaxLength(50, ErrorMessage = "Unit name can have 50 characters maximum!")]

        public string WorkLocationName { get; set; }
        [DisplayName("SAP Code")]
        [RegularExpression(@"^[A-Za-z0-9]+", ErrorMessage = "Only AlphaNumeric values are allowed!")]
        [MaxLength(50, ErrorMessage = "SAP Code can have 50 characters maximum!")]

        public string SAPCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_Emp_Details> HRMS_Emp_Details { get; set; }
    }
}
