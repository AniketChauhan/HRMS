using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
using HRMS.ViewModel;

namespace HRMS.Controllers
{
    public class EmployeeSelectionController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingEmployeeSelection trainingEmployeeSelection = new TrainingEmployeeSelection();
            HRMS_ProgramDetail programDetail = db.HRMS_ProgramDetail.Find(id);
            //List< HRMS_TrainingApproval> employeeList = db.HRMS_TrainingApproval.Where(rec=>rec.Program_ID == id).ToList();
            if (programDetail == null)
            {
                return HttpNotFound();
            }
            trainingEmployeeSelection.programDetail = programDetail;
            //if (employeeList != null)
            //{
            //    trainingEmployeeSelection.employeeDataList = employeeList;
            //}
            return View(trainingEmployeeSelection);
        }
    }
}