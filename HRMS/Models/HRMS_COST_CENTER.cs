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
    using System.ComponentModel.DataAnnotations;

    public partial class HRMS_COST_CENTER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRMS_COST_CENTER()
        {
            this.HRMS_Emp_Details = new HashSet<HRMS_Emp_Details>();
        }

        public long ID { get; set; }
        [Required]
        [Display(Name = "Cost Center Code")]
        [Range(1, 2147483647, ErrorMessage ="Code should be 10 digits maximum!")]
        public long Cost_Cntr_Code { get; set; }
        [Required]
        [Display(Name = "Cost Center Name")]
        [RegularExpression(@"^[A-Za-z]+", ErrorMessage = "Only Alphabetic values are allowed!")]
        [DataType(DataType.Text, ErrorMessage = "Only Alphabetic value are allowed")]
        [MaxLength(50, ErrorMessage = "Cost Center name can have 50 characters maximum!")]

        public string Cost_Cntr_Name { get; set; }
        [Display(Name = "Parent Cost Center Code")]
        [Range(1, 2147483647, ErrorMessage = "Parent Code should be 10 digits maximum!")]


        public Nullable<long> Parent_Cost_Cntr_Code { get; set; }
        [Display(Name = "Ledger Code")]
        [Range(1, 2147483647, ErrorMessage = "Ledger Code should be 10 digits maximum!")]

        public Nullable<long> Ledger_Code { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_Emp_Details> HRMS_Emp_Details { get; set; }
    }
}
