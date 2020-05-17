﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class HRMSEntities : DbContext
    {
        public HRMSEntities()
            : base("name=HRMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<BankMaster> BankMaster { get; set; }
        public virtual DbSet<BranchMaster> BranchMaster { get; set; }
        public virtual DbSet<CastMaster> CastMaster { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<EMP_Grade_Assignment> EMP_Grade_Assignment { get; set; }
        public virtual DbSet<Employee_Personal_Detail> Employee_Personal_Detail { get; set; }
        public virtual DbSet<HRMS_ATTACHMENT_TYPE> HRMS_ATTACHMENT_TYPE { get; set; }
        public virtual DbSet<HRMS_CATEGORY_GRADE> HRMS_CATEGORY_GRADE { get; set; }
        public virtual DbSet<HRMS_Contact> HRMS_Contact { get; set; }
        public virtual DbSet<HRMS_COST_CENTER> HRMS_COST_CENTER { get; set; }
        public virtual DbSet<HRMS_DEPT> HRMS_DEPT { get; set; }
        public virtual DbSet<HRMS_EMP_Attachment_Details> HRMS_EMP_Attachment_Details { get; set; }
        public virtual DbSet<HRMS_EMP_BUSINESSDIVISION_MS> HRMS_EMP_BUSINESSDIVISION_MS { get; set; }
        public virtual DbSet<HRMS_EMP_CITIZENSHIP_MS> HRMS_EMP_CITIZENSHIP_MS { get; set; }
        public virtual DbSet<HRMS_EMP_GENDER_MS> HRMS_EMP_GENDER_MS { get; set; }
        public virtual DbSet<HRMS_EMP_PHOTO_SIGN> HRMS_EMP_PHOTO_SIGN { get; set; }
        public virtual DbSet<HRMS_EMP_ReferenceDetail> HRMS_EMP_ReferenceDetail { get; set; }
        public virtual DbSet<HRMS_SALUTATION> HRMS_SALUTATION { get; set; }
        public virtual DbSet<HRMS_Travel_Purpose> HRMS_Travel_Purpose { get; set; }
        public virtual DbSet<HRMS_Travel_type> HRMS_Travel_type { get; set; }
        public virtual DbSet<HRMS_TravelGroupType_MS> HRMS_TravelGroupType_MS { get; set; }
        public virtual DbSet<HRMS_TravelMode_MS> HRMS_TravelMode_MS { get; set; }
        public virtual DbSet<MaritalMaster> MaritalMaster { get; set; }
        public virtual DbSet<PayRollMaster> PayRollMaster { get; set; }
        public virtual DbSet<ReligionMaster> ReligionMaster { get; set; }
        public virtual DbSet<SegmentMaster> SegmentMaster { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<UnitMaster> UnitMaster { get; set; }
        public virtual DbSet<WorkLocationMaster> WorkLocationMaster { get; set; }
        public virtual DbSet<HRMS_DESG_MS> HRMS_DESG_MS { get; set; }
        public virtual DbSet<HRMS_Emp_Details> HRMS_Emp_Details { get; set; }
        public virtual DbSet<HRMS_TRAVEL_MEAL_EXPENSE_MS> HRMS_TRAVEL_MEAL_EXPENSE_MS { get; set; }
        public virtual DbSet<HRMS_TRAVEL_OTHER_DETAILS_MS> HRMS_TRAVEL_OTHER_DETAILS_MS { get; set; }
        public virtual DbSet<HRMS_Travel_Application> HRMS_Travel_Application { get; set; }
        public virtual DbSet<Sample> Sample { get; set; }
        public virtual DbSet<HRMS_Travel_Expense_App> HRMS_Travel_Expense_App { get; set; }
    
        public virtual ObjectResult<DepartmentData_Result> DepartmentData()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DepartmentData_Result>("DepartmentData");
        }
    
        public virtual ObjectResult<FillEmployee_Result> FillEmployee()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FillEmployee_Result>("FillEmployee");
        }
    
        public virtual ObjectResult<EmployeeReport_Result> EmployeeReport(Nullable<long> department, Nullable<long> workLocation, Nullable<System.DateTime> fromdate, Nullable<System.DateTime> todate)
        {
            var departmentParameter = department.HasValue ?
                new ObjectParameter("department", department) :
                new ObjectParameter("department", typeof(long));
    
            var workLocationParameter = workLocation.HasValue ?
                new ObjectParameter("WorkLocation", workLocation) :
                new ObjectParameter("WorkLocation", typeof(long));
    
            var fromdateParameter = fromdate.HasValue ?
                new ObjectParameter("fromdate", fromdate) :
                new ObjectParameter("fromdate", typeof(System.DateTime));
    
            var todateParameter = todate.HasValue ?
                new ObjectParameter("todate", todate) :
                new ObjectParameter("todate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeReport_Result>("EmployeeReport", departmentParameter, workLocationParameter, fromdateParameter, todateParameter);
        }
    }
}
