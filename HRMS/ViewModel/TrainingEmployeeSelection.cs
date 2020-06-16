using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;

namespace HRMS.ViewModel
{
    public class TrainingEmployeeSelection
    {
        public HRMS_ProgramDetail programDetail { get; set; }
        public HRMS_TrainingApproval employeeSelection { get; set; }
        public List<HRMS_TrainingApproval> employeeDataList { get; set; }
    }
}