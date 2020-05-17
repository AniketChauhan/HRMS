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

    public partial class HRMS_Emp_Details
    {
        public long ID { get; set; }
        [Required]
        [Display(Name = "Employee Code")]

        public long EMP_ID { get; set; }
        [Display(Name = "Old Employee Code")]
        [RegularExpression(@"^[0-9]$", ErrorMessage = "Old Employee Code must be in valid format")]

        public Nullable<long> Old_Emp_Cd { get; set; }
        [Required]
        [Display(Name = "Join Date")]
        // [DataType(DataType.Date, ErrorMessage = "Only Date allowed")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Join_Date { get; set; }
        [Display(Name = "Card Id")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Card Id must be in valid format")]

        public Nullable<long> Card_Id { get; set; }
        [Required]
        [Display(Name = "salutation")]
        public long salutation { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[A-Za-z]+", ErrorMessage = "Only Alphabetic values are allowed!")]
        [DataType(DataType.Text, ErrorMessage = "Only Alphabetic value are allowed")]
        [MaxLength(50, ErrorMessage = "First name can have 50 characters maximum!")]
        public string First_Name { get; set; }
        [Display(Name = "Middle Name")]
        [RegularExpression(@"^[A-Za-z]+", ErrorMessage = "Only Alphabetic values are allowed!")]
        [DataType(DataType.Text, ErrorMessage = "Only Alphabetic value are allowed")]
        [MaxLength(50, ErrorMessage = "Middle name can have 50 characters maximum!")]
        public string Middle_Name { get; set; }
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[A-Za-z]+", ErrorMessage = "Only Alphabetic values are allowed!")]
        [DataType(DataType.Text, ErrorMessage = "Only Alphabetic value are allowed")]
        [MaxLength(50, ErrorMessage = "Last name can have 50 characters maximum!")]
        public string Last_Name { get; set; }
        [Required]
        [Display(Name = "Display Name")]
        [RegularExpression(@"^[A-Za-z_ ]*$", ErrorMessage = "Only Alphabetic values are allowed!")]
        [DataType(DataType.Text, ErrorMessage = "Only Alphabetic value are allowed")]
        [MaxLength(250, ErrorMessage = "Display name can have 250 characters maximum!")]
        public string Display_Name { get; set; }
        [Required]
        [Display(Name = "Designation")]
        public long Designation { get; set; }
        [Display(Name = "Work Location")]

        public Nullable<long> Work_Location { get; set; }
        [Required]
        [Display(Name = "Unit")]
        public long Unit { get; set; }
        [Required]
        [Display(Name = "Department")]
        public long Department { get; set; }
        [Required]
        [Display(Name = "Cost Center")]
        public long Cost_Center { get; set; }
        [Display(Name = "UAN No")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "UAN No must be in valid format")]
        public Nullable<long> UAN_No_ { get; set; }

        public virtual Accounts Accounts { get; set; }
        public virtual HRMS_COST_CENTER HRMS_COST_CENTER { get; set; }
        public virtual HRMS_DEPT HRMS_DEPT { get; set; }
        public virtual HRMS_DESG_MS HRMS_DESG_MS { get; set; }
        public virtual HRMS_SALUTATION HRMS_SALUTATION { get; set; }
        public virtual UnitMaster UnitMaster { get; set; }
        public virtual WorkLocationMaster WorkLocationMaster { get; set; }
    }
}
