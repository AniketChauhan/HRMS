using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
namespace HRMS.Controllers
{
    public class TrainingAttandanceController : Controller
    {
        HRMSEntities db = new HRMSEntities();

        // GET: TrainingAttandance
        public ActionResult Index()
        {
            long emp_id = Convert.ToInt64(Session["id"]);
            var ProgramList = db.HRMS_ProgramFaculty.Where(rec => rec.FacultyID == emp_id).ToList();
          
            return View(ProgramList);
        }
        
       public ActionResult EmployeeAttandance(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TrainingApproval Program = db.HRMS_TrainingApproval.Where(rec=>rec.ID == id).FirstOrDefault();

            if (Program == null)
            {
                return HttpNotFound();
            }
            return View(Program);
        }
    }
}