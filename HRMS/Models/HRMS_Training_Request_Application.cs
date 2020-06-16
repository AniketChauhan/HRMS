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

    public partial class HRMS_Training_Request_Application
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HRMS_Training_Request_Application()
        {
            this.HRMS_ProgramDetail = new HashSet<HRMS_ProgramDetail>();
        }

        public long ApplicationId { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime request_date { get; set; }
        public long EmpID { get; set; }
        public string EmployeeName { get; set; }

        public Nullable<long> designationID { get; set; }
        public Nullable<long> DepartmentId { get; set; }
        public string Training_Name { get; set; }
        public string TrainingDetails { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<long> ApprovedBy_ID { get; set; }
        public string Approved_Remarks { get; set; }
        public long Skill { get; set; }
        public Nullable<long> ProgramFlag { get; set; }
        public virtual Accounts Accounts { get; set; }
        public virtual Accounts Accounts1 { get; set; }
        public virtual HRMS_DEPT HRMS_DEPT { get; set; }
        public virtual HRMS_DESG_MS HRMS_DESG_MS { get; set; }
        public virtual HRMS_TRAINING_SKILL_MS HRMS_TRAINING_SKILL_MS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_ProgramDetail> HRMS_ProgramDetail { get; set; }
    }
}
