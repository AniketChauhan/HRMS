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

    public partial class HRMS_Travel_Purpose
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRMS_Travel_Purpose()
        {
            this.HRMS_Travel_Application = new HashSet<HRMS_Travel_Application>();
        }

        public long ID { get; set; }
        [Required]
        [Display(Name = "Name")]
        [RegularExpression(@"^[A-Za-z_ ]*$", ErrorMessage = "Only Alphabetic values are allowed!")]
        [DataType(DataType.Text, ErrorMessage = "Only Alphabetic value are allowed")]
        [MaxLength(250, ErrorMessage = "Display name can have 250 characters maximum!")]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_Travel_Application> HRMS_Travel_Application { get; set; }
    }
}
