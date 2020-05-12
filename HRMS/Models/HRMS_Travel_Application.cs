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
    
    public partial class HRMS_Travel_Application
    {
        public long ID { get; set; }
        public long Travel_App_Type { get; set; }
        public System.DateTime Travel_Application_Date { get; set; }
        public long emp_id { get; set; }
        public Nullable<long> Designation { get; set; }
        public Nullable<long> Grade { get; set; }
        public int Travel_Type { get; set; }
        public long Travel_Purpose { get; set; }
        public System.DateTime From_Date { get; set; }
        public System.DateTime To_Date { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public Nullable<long> Approved_by { get; set; }
        public Nullable<System.DateTime> Approved_date { get; set; }
        public string Approval_Remark { get; set; }
    
        public virtual Accounts Accounts { get; set; }
        public virtual Accounts Accounts1 { get; set; }
        public virtual HRMS_CATEGORY_GRADE HRMS_CATEGORY_GRADE { get; set; }
        public virtual HRMS_DESG_MS HRMS_DESG_MS { get; set; }
        public virtual HRMS_Travel_Purpose HRMS_Travel_Purpose { get; set; }
        public virtual HRMS_Travel_type HRMS_Travel_type { get; set; }
        public virtual HRMS_TravelMode_MS HRMS_TravelMode_MS { get; set; }
    }
}
