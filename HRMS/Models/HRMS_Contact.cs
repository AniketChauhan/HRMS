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
    
    public partial class HRMS_Contact
    {
        public long ID { get; set; }
        public string Phone_Work { get; set; }
        public string Ext { get; set; }
        public string Mobile_No_Work { get; set; }
        public string Corporate_Email { get; set; }
        public string Phone_Home { get; set; }
        public string Mobile_no_home { get; set; }
        public string Personal_Email { get; set; }
        public string Office_Email { get; set; }
        public long Employee_ID { get; set; }
    
        public virtual Accounts Accounts { get; set; }
    }
}
