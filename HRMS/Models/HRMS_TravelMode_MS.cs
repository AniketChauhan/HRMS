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
    
    public partial class HRMS_TravelMode_MS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRMS_TravelMode_MS()
        {
            this.HRMS_TravelGroupType_MS = new HashSet<HRMS_TravelGroupType_MS>();
            this.HRMS_Travel_Application = new HashSet<HRMS_Travel_Application>();
        }
        public long Mode_ID { get; set; }
        [Display(Name ="Mode Type")]
        public string Mode_Type { get; set; }
        [Required]
        [Display(Name = "Mode Name")]
        [RegularExpression(@"^[A-Za-z_ ]*$", ErrorMessage = "Only Alphabetic values are allowed!")]
        [DataType(DataType.Text, ErrorMessage = "Only Alphabetic value are allowed")]
        [MaxLength(200, ErrorMessage = "Mode name can have 200 characters maximum!")]
        public string Mode_Name { get; set; }
        [Required]
        [Display(Name = "Mode Short Name")]
        [RegularExpression(@"^[A-Za-z0-9_ ]*$", ErrorMessage = "Only AlphNumeric values are allowed!")]
        [DataType(DataType.Text, ErrorMessage = "Only Alphabetic value are allowed")]
        [MaxLength(100, ErrorMessage = "Mode Short name can have 100 characters maximum!")]
        public string Mode_Short_Name { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_TravelGroupType_MS> HRMS_TravelGroupType_MS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_Travel_Application> HRMS_Travel_Application { get; set; }
    }
}
