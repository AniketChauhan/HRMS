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
    
    public partial class HRMS_TrainingApproval
    {
        public long ID { get; set; }
        public long EMP_ID { get; set; }
        public Nullable<long> Designation { get; set; }
        public long Program_ID { get; set; }
        public Nullable<long> ApproveBy { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
        public Nullable<long> Status { get; set; }
        public string EmpName { get; set; }
        public Nullable<long> EmpDept { get; set; }
    
        public virtual Accounts Accounts { get; set; }
        public virtual HRMS_DEPT HRMS_DEPT { get; set; }
        public virtual HRMS_DESG_MS HRMS_DESG_MS { get; set; }
        public virtual HRMS_ProgramDetail HRMS_ProgramDetail { get; set; }
    }
}
