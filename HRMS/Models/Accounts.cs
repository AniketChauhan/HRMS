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
    
    public partial class Accounts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Accounts()
        {
            this.EMP_Grade_Assignment = new HashSet<EMP_Grade_Assignment>();
            this.Employee_Personal_Detail = new HashSet<Employee_Personal_Detail>();
            this.HRMS_Contact = new HashSet<HRMS_Contact>();
            this.HRMS_EMP_Attachment_Details = new HashSet<HRMS_EMP_Attachment_Details>();
            this.HRMS_EMP_PHOTO_SIGN = new HashSet<HRMS_EMP_PHOTO_SIGN>();
            this.HRMS_EMP_ReferenceDetail = new HashSet<HRMS_EMP_ReferenceDetail>();
            this.HRMS_Emp_Details = new HashSet<HRMS_Emp_Details>();
            this.HRMS_Travel_Application = new HashSet<HRMS_Travel_Application>();
            this.HRMS_Travel_Application1 = new HashSet<HRMS_Travel_Application>();
            this.Sample = new HashSet<Sample>();
            this.HRMS_Travel_Expense_App = new HashSet<HRMS_Travel_Expense_App>();
            this.HRMS_EMP_GRA_POL = new HashSet<HRMS_EMP_GRA_POL>();
        }
    
        public long ID { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string ConfirmUsername { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_Grade_Assignment> EMP_Grade_Assignment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee_Personal_Detail> Employee_Personal_Detail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_Contact> HRMS_Contact { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_EMP_Attachment_Details> HRMS_EMP_Attachment_Details { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_EMP_PHOTO_SIGN> HRMS_EMP_PHOTO_SIGN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_EMP_ReferenceDetail> HRMS_EMP_ReferenceDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_Emp_Details> HRMS_Emp_Details { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_Travel_Application> HRMS_Travel_Application { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_Travel_Application> HRMS_Travel_Application1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sample> Sample { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_Travel_Expense_App> HRMS_Travel_Expense_App { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HRMS_EMP_GRA_POL> HRMS_EMP_GRA_POL { get; set; }
    }
}
