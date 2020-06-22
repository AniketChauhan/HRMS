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
            long facID = db.HRMS_Faculty_MS.Where(x=>x.EMP_ID==emp_id).Select(x=>x.ID).FirstOrDefault();
            var ProgramList = db.HRMS_ProgramFaculty.Where(rec => rec.FacultyID == facID).ToList();

            return View(ProgramList);
        }
        public ActionResult View(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var attandance = db.HRMS_Program_Attandance.Where(r => r.ProgramID == id).ToList();
            //var Program = db.HRMS_ProgramDetail.Where(r => r.ID == id).FirstOrDefault();
            //ViewBag.FromDate = Program.FromDate;
            //ViewBag.ToDate = Program.ToDate;
            //ViewBag.ProgramID = Program.ID;
            //ViewBag.ProgramName = ;
            return View(attandance);

        }

        public ActionResult EmployeeAttandance(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TrainingApproval Program = db.HRMS_TrainingApproval.Where(rec => rec.Program_ID == id).FirstOrDefault();
            if (Program == null)
            {
                return HttpNotFound();
            }
            ViewBag.FromDate = Program.FromDate;
            ViewBag.ToDate = Program.ToDate;
            ViewBag.ProgramID = Program.Program_ID;
            return View();
        }
        public JsonResult GetEmployees(string ProgramId, DateTime datee)
        {
            db.Configuration.ProxyCreationEnabled = false;
            long program = Convert.ToInt64(ProgramId);

            List<HRMS_TrainingApproval> EmployeeList = db.HRMS_TrainingApproval.Where(x => x.Program_ID == program && x.Status == 2 && x.FromDate <= datee && x.ToDate >= datee).ToList();

            if (EmployeeList.Count != 0)
            {
                return Json(EmployeeList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var Nodata = "No Data";
                return Json(Nodata, JsonRequestBehavior.AllowGet);

            }
        }
        public void insertAttandance(long id, string Status, DateTime date)
        {
            var employee = db.HRMS_TrainingApproval.Where(rec => rec.ID == id).FirstOrDefault();
            var empdata = db.HRMS_Emp_Details.Where(rec => rec.EMP_ID == employee.EMP_ID).FirstOrDefault();
            var emppersonal = db.Employee_Personal_Detail.Where(rec => rec.EMP_ID == employee.EMP_ID).FirstOrDefault();

            if (employee != null)
            {
                HRMS_Program_Attandance attandance = new HRMS_Program_Attandance();
                attandance.ProgramID = employee.Program_ID;
                attandance.EmployeeID = employee.EMP_ID;
                attandance.IsPresent = Status;
                attandance.AttandenceDate = date;
                attandance.DesignationID = empdata.Designation;
                attandance.DepartmentID = empdata.Department;
                attandance.EmpName = empdata.Display_Name;
                if (emppersonal != null)
                {
                    if (emppersonal.Gender == 1)
                    {
                        attandance.Gender = "Male";
                    }
                    else if (emppersonal.Gender == 2)
                    {
                        attandance.Gender = "female";
                    }
                }
                var already_exist = db.HRMS_Program_Attandance.Where(r => r.ProgramID == attandance.ProgramID && r.EmployeeID == attandance.EmployeeID && r.AttandenceDate == attandance.AttandenceDate).FirstOrDefault();
                if (already_exist == null)
                {
                    db.HRMS_Program_Attandance.Add(attandance);
                    db.SaveChanges();
                    ModelState.Clear();
                }
            }
        }
    }
}