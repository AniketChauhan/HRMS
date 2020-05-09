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


    public partial class PayRollMaster
    {
        //public long PayRollCode { get; set; }
        //public string PayRollName { get; set; }

        public long PayRollCode { get; set; }
        [Required]
        [DisplayName("PayRoll Name")]
        [RegularExpression(@"^[A-Za-z]+", ErrorMessage = "Only Alphabet values are allowed!")]
        public string PayRollName { get; set; }
    }
}
