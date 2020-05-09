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

    public partial class BranchMaster
    {
        [DisplayName("Bank Branch Code")]
        public long BranchCode { get; set; }
        [DisplayName("Bank Name")]
        public long BankCode { get; set; }
        [DisplayName("Bank Branch Name")]
        public string BranchName { get; set; }
        [DisplayName("MICR Code")]
        public string MICR_Code { get; set; }
        [DisplayName("IFSC Code")]
        public string IFSC_Code { get; set; }
        [DisplayName("Contact Number")]
        public Nullable<long> Contact { get; set; }
        [DisplayName("Fax Number")]
        public string Fax { get; set; }
        [DisplayName("Email ID")]
        public string EmailID { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public Nullable<long> PinCode { get; set; }

        public virtual BankMaster BankMaster { get; set; }
    }
}
