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

    public partial class HRMS_Contact
    {
        public long ID { get; set; }
        [Required]
        [Display(Name ="Phone Work")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone Work no. must be in valid format")]

        public string Phone_Work { get; set; }
        [RegularExpression(@"^[0-9]{3}$", ErrorMessage = "Ext no. must be in valid format")]

        public string Ext { get; set; }
        [Required]
        [Display(Name = "Phone No. Work")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone No. Work must be in valid format")]

        public string Mobile_No_Work { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Corporate Email")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Enter a valid email !")]

        public string Corporate_Email { get; set; }
        [Display(Name = "Phone Home")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone Home must be in valid format")]

        public string Phone_Home { get; set; }
        [Display(Name = "Phone No. Home")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone no. Home must be in valid format")]

        public string Mobile_no_home { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Personal Email")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Enter a valid email !")]
        public string Personal_Email { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Office Email ID")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Enter a valid email !")]
        public string Office_Email { get; set; }
        public long Employee_ID { get; set; }
    
        public virtual Accounts Accounts { get; set; }
    }
}
