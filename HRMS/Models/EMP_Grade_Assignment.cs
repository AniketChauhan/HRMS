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
    
    public partial class EMP_Grade_Assignment
    {
        public long ID { get; set; }
        public long EMP_ID { get; set; }
        public long Grade_ID { get; set; }
    
        public virtual Accounts Accounts { get; set; }
        public virtual HRMS_CATEGORY_GRADE HRMS_CATEGORY_GRADE { get; set; }
    }
}