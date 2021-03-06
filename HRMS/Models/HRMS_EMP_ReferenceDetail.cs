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

    public partial class HRMS_EMP_ReferenceDetail
    {
        public long ID { get; set; }
        [DisplayName("Employee ID")]
        public long EMP_ID { get; set; }
        [DisplayName("If Employee as Reference")]
        public bool Is_Ref_Emp { get; set; }
        [Required]
        [Display(Name = "Name")]
        [RegularExpression(@"^[A-Za-z_ ]*$", ErrorMessage = "Only Alphabetic values are allowed!")]
        [DataType(DataType.Text, ErrorMessage = "Only Alphabetic value are allowed")]
        [MaxLength(100, ErrorMessage = "name can have 100 characters maximum!")]
        public string Name { get; set; }
        public long Country { get; set; }
        public long State { get; set; }
        public long City { get; set; }
        [Required]
        [Display(Name = "Relationship")]
        [RegularExpression(@"^[A-Za-z_ ]*$", ErrorMessage = "Only Alphabetic values are allowed!")]
        [DataType(DataType.Text, ErrorMessage = "Only Alphabetic value are allowed")]
        [MaxLength(50, ErrorMessage = "Relationship can have 50 characters maximum!")]
        public string Relationship { get; set; }
        [RegularExpression(@"^[1-9][0-9]{6}$", ErrorMessage = "Pincode must be in valid format")]
        public Nullable<long> Pincode { get; set; }
        public string Address { get; set; }

        [Display(Name = "Designation")]
        [RegularExpression(@"^[A-Za-z_ ]*$", ErrorMessage = "Only Alphabetic values are allowed!")]
        [DataType(DataType.Text, ErrorMessage = "Only Alphabetic value are allowed")]
        [MaxLength(50, ErrorMessage = "Designation can have 50 characters maximum!")]

        public string Designation { get; set; }
        [Display(Name = "Company")]
        [RegularExpression(@"^[A-Za-z_ ]*$", ErrorMessage = "Only Alphabetic values are allowed!")]
        [DataType(DataType.Text, ErrorMessage = "Only Alphabetic value are allowed")]
        [MaxLength(50, ErrorMessage = "Company can have 50 characters maximum!")]

        public string Company { get; set; }
        [DisplayName("Phone No.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone no. must be in valid format")]

        public string PhoneNo { get; set; }
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Mobile no. must be in valid format")]
        [DisplayName("Mobile No.")]
        [DataType(DataType.PhoneNumber)]

        public string MobileNo { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [Display(Name = "Email")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Enter a valid email !")]

        public string Email { get; set; }


        public virtual Accounts Accounts { get; set; }
        public virtual City City1 { get; set; }
        public virtual Country Country1 { get; set; }
        public virtual State State1 { get; set; }
    }
}
