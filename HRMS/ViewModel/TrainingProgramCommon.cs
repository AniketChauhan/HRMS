using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;


namespace HRMS.ViewModel
{
    public class TrainingProgramCommon
    {
       
            public HRMS_ProgramFaculty ProFaculty { get; set; }
            public HRMS_ProgramDetail ProDetail { get; set; }
            public List<HRMS_ProgramFaculty> FacultyList { get; set; }
       }
}