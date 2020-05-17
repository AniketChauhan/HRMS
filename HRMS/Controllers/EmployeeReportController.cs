using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
namespace HRMS.Controllers
{
    public class EmployeeReportController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: EmployeeReport
        public ActionResult Index()
        {
            ViewBag.departments = ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
            ViewBag.WorkLocation = db.WorkLocationMaster;

            return View();
        }
        //public ActionResult search(long Department,long Work_Location,DateTime FromDate,DateTime ToDate)
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            string Departmentt = collection["Department"];
            string WorkLocationn = collection["WorkLocation"];
            string fromdatee = collection["FromDate"];
            string todatee = collection["ToDate"];
            long? Department = null;
            long? Work_Location = null;
            DateTime? FromDate = null;
            DateTime? ToDate = null;
            if (Departmentt != "")
            {
                Department = Convert.ToInt64(collection["Department"]);
            }
            if (WorkLocationn != "")
            {
                Work_Location = Convert.ToInt64(collection["WorkLocation"]);
            }
            if (fromdatee != "")
            {
                FromDate = Convert.ToDateTime(collection["FromDate"]);
            }
            if (todatee != "")
            {
                ToDate = Convert.ToDateTime(collection["ToDate"]);
            }
            //long? Department = Convert.ToInt64(collection["Department"]);
            //long? Work_Location = Convert.ToInt64(collection["WorkLocation"]);
            //DateTime? FromDate = Convert.ToDateTime(collection["FromDate"]);
            //DateTime? ToDate = Convert.ToDateTime(collection["ToDate"]);

            if (FromDate != null && ToDate == null)
            {
                ViewBag.EmployeeReportStatus = "please enter ToDate!";
                ViewBag.departments = ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
                ViewBag.WorkLocation = db.WorkLocationMaster;
                return View();
            }
            if (FromDate == null && ToDate != null)
            {
                ViewBag.EmployeeReportStatus = "please enter FromDate!";
                ViewBag.departments = ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
                ViewBag.WorkLocation = db.WorkLocationMaster;
                return View();
            }
            if (Department == null && Work_Location == null && FromDate == null && ToDate == null)
            {
                ViewBag.EmployeeReportStatus = "Please enter anyone option!";
                ViewBag.departments = ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
                ViewBag.WorkLocation = db.WorkLocationMaster;
                return View();
            }
            
            var modeldata = db.EmployeeReport(Department, Work_Location, FromDate, ToDate).ToList();
            ViewBag.departments = ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
            ViewBag.WorkLocation = db.WorkLocationMaster;
            ViewBag.departments = ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
            ViewBag.WorkLocation = db.WorkLocationMaster;
            return View(modeldata);

        }
    }
}