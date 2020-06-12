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
    
    public partial class HRMS_ProgramDetail
    {
        public long ID { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public long TrainingID { get; set; }
        public string ProgramName { get; set; }
        public string Subject { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public System.TimeSpan FromTime { get; set; }
        public System.TimeSpan ToTime { get; set; }
        public string TrainingType { get; set; }
        public string ProgramType { get; set; }
        public string SubjectType { get; set; }
        public string Type { get; set; }
        public string ProgramMode { get; set; }
        public string Venue { get; set; }
        public decimal Budget { get; set; }
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
    }
}
