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
    using System.ComponentModel.DataAnnotations;

    public partial class EmployeeReport_Result
    {
        public long EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        [DataType(DataType.Date)]

        public System.DateTime JoinDate { get; set; }
        public string Department { get; set; }
        public string WorkLocation { get; set; }
        public string Gender_Value { get; set; }
        [DataType(DataType.Date)]

        public System.DateTime DateOfBirth { get; set; }
    }
}
