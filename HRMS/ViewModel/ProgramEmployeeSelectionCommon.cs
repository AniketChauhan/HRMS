using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HRMS.Models;

namespace HRMS.ViewModel
{
    public class ProgramEmployeeSelectionCommon
    {
        public HRMS_ProgramDetail programDetail { get; set; }
        public HRMS_TrainingApproval employeeselection { get; set; }
        public List<HRMS_TrainingApproval> EmoloyeedataList { get; set; }
    }
}