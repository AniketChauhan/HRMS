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

    public partial class HRMS_ProgramDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRMS_ProgramDetail()
        {
            this.HRMS_ProgramFaculty = new HashSet<HRMS_ProgramFaculty>();
            this.HRMS_TrainingApproval = new HashSet<HRMS_TrainingApproval>();
        }

        public long ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime TransactionDate { get; set; }
        public long TrainingID { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]

        public string ProgramName { get; set; }
        [Required]
        public string Subject { get; set; }
        //public System.DateTime FromDate { get; set; }
        //public System.DateTime ToDate { get; set; }
        //public System.TimeSpan FromTime { get; set; }
        //public System.TimeSpan ToTime { get; set; }
        [Required]
        public string TrainingType { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required]
        public Nullable<System.DateTime> FromDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required]
        public Nullable<System.DateTime> ToDate { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:H:mm}")]
        [Required]
        public Nullable<System.TimeSpan> FromTime { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:H:mm}")]
        [Required]
        public Nullable<System.TimeSpan> ToTime { get; set; }
        [Required]
        public string ProgramType { get; set; }
        [Required]
        public string SubjectType { get; set; }

        public string Type { get; set; }

        public string ProgramMode { get; set; }
        [Required]
        public string Venue { get; set; }
        [Required]
        public Nullable<decimal> Budget { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string BenefitsToOrg { get; set; }
        public string Remarks { get; set; }
        public string RatingMethod { get; set; }
        public string TrainingStatus { get; set; }
        public Nullable<long> CompletedBy { get; set; }
        public Nullable<System.DateTime> CompleteDate { get; set; }
        public string RemarksOther { get; set; }
        public Nullable<long> Flag { get; set; }

        //extra
        public string Extra { get; set; }

        public virtual HRMS_Training_Request_Application HRMS_Training_Request_Application { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_ProgramFaculty> HRMS_ProgramFaculty { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_TrainingApproval> HRMS_TrainingApproval { get; set; }
    }
}
