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



    public partial class HRMS_TravelGroupType_MS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRMS_TravelGroupType_MS()
        {
            this.HRMS_EMP_GRA_POL = new HashSet<HRMS_EMP_GRA_POL>();
        }
       
        public long ID { get; set; }
        [DisplayName("Type")]
        public long Mode_ID { get; set; }
        [DisplayName("Effective Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required]
        public System.DateTime Date { get; set; }
        [Required]
        [DisplayName("Group Name")]
        [RegularExpression(@"^[A-Za-z_ ]*$", ErrorMessage = "Only Alphabetic values are allowed!")]
        [MaxLength(100, ErrorMessage = "name can have 100 characters maximum!")]

        public string Group_Name { get; set; }
    
        public virtual HRMS_TravelMode_MS HRMS_TravelMode_MS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_EMP_GRA_POL> HRMS_EMP_GRA_POL { get; set; }
    }
}
