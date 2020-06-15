using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;


namespace HRMS.ViewModel
{
    public class EvalutionCommon
    {
        public HRMS_Evalution_Question question { get; set; }
        public HRMS_TrainingEvalution_Header header { get; set; }
        public List<HRMS_Evalution_Question> QuestionLists { get; set; }
    }

  
   
}