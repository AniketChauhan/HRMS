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

    public partial class HRMS_EMP_GENDER_MS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRMS_EMP_GENDER_MS()
        {
            this.Employee_Personal_Detail = new HashSet<Employee_Personal_Detail>();
        }
        [DisplayName("Gender ID")]
        public long Gender_ID { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [DisplayName("Gender")]
        [MaxLength(10, ErrorMessage = "Designation ShortName can have 10 characters maximum!")]

        public string Gender_Value { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee_Personal_Detail> Employee_Personal_Detail { get; set; }
    }
}
