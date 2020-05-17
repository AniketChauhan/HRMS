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

    public partial class HRMS_CATEGORY_GRADE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRMS_CATEGORY_GRADE()
        {
            this.EMP_Grade_Assignment = new HashSet<EMP_Grade_Assignment>();
            this.Employee_Personal_Detail = new HashSet<Employee_Personal_Detail>();
            this.HRMS_Travel_Application = new HashSet<HRMS_Travel_Application>();
            this.HRMS_EMP_GRA_POL = new HashSet<HRMS_EMP_GRA_POL>();
        }
    
        public long Category_ID { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        [RegularExpression(@"^[A-Za-z]+", ErrorMessage = "Only Alphabetic values are allowed!")]
        [DataType(DataType.Text, ErrorMessage = "Only Alphabetic value are allowed")]
        [MaxLength(20, ErrorMessage = "Category name can have 20 characters maximum!")]
        public string Category_Name { get; set; }
        [Required]
        [Display(Name = "Grade Name")]
        [RegularExpression(@"^[A-Za-z]+", ErrorMessage = "Only Alphabetic values are allowed!")]
        [DataType(DataType.Text, ErrorMessage = "Only Alphabetic value are allowed")]
        [MaxLength(20, ErrorMessage = "Grade name can have 20 characters maximum!")]
        public string Grade_Name { get; set; }
        [Display(Name = "Grade Detail")]
        [RegularExpression(@"^[A-Za-z]+", ErrorMessage = "Only Alphabetic values are allowed!")]
        [DataType(DataType.Text, ErrorMessage = "Only Alphabetic value are allowed")]
        [MaxLength(150, ErrorMessage = "Grade Detail can have 150 characters maximum!")]
        public string Grade_Detail { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_Grade_Assignment> EMP_Grade_Assignment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee_Personal_Detail> Employee_Personal_Detail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_Travel_Application> HRMS_Travel_Application { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_EMP_GRA_POL> HRMS_EMP_GRA_POL { get; set; }
    }
}
