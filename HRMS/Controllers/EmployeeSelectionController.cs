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

        public JsonResult GetEmployees(string ProgramId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            long program = Convert.ToInt64(ProgramId);

            List<HRMS_TrainingApproval> EmployeeList = db.HRMS_TrainingApproval.Where(x => x.Program_ID == program && x.Status==2).ToList();
            if (EmployeeList != null)
            {
                return Json(EmployeeList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var Nodata = "No Data";
                return Json(Nodata, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult EditEmployeeSelection(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //TrainingEmployeeSelection trainingEmployeeSelection = new TrainingEmployeeSelection();
            HRMS_ProgramDetail programDetail = db.HRMS_ProgramDetail.Find(id);
            //List< HRMS_TrainingApproval> employeeList = db.HRMS_TrainingApproval.Where(rec=>rec.Program_ID == id).ToList();
            if (programDetail == null)
            {
                return HttpNotFound();
            }
            //trainingEmployeeSelection.programDetail = programDetail;
            ViewBag.ID = programDetail.ID;
            ViewBag.ProgramName = programDetail.ProgramName;

            ViewBag.ProgramType = programDetail.ProgramType;

            ViewBag.TrainingType = programDetail.TrainingType;

            ViewBag.FromDate = programDetail.FromDate;

            ViewBag.ToDate = programDetail.ToDate;

            ViewBag.FromTime = programDetail.FromTime;

            ViewBag.ToTime = programDetail.ToTime;

            ViewBag.TransactionDate = programDetail.TransactionDate;


            HRMS_TrainingApproval TrainingApproval = new HRMS_TrainingApproval();
            TrainingApproval.FromDate = programDetail.FromDate;
            TrainingApproval.ToDate = programDetail.ToDate;
            TrainingApproval.Program_ID = programDetail.ID;
            TrainingApproval.FromTime = programDetail.FromTime;

            TrainingApproval.ToTime = programDetail.ToTime;
            //trainingEmployeeSelection.employeeSelection = TrainingApproval;

            //if (employeeList != null)
            //{
            //    trainingEmployeeSelection.employeeDataList = employeeList;
            //}
            return View(TrainingApproval);
        }
        [HttpPost]
        public JsonResult EditEmployeeSelectiondata(HRMS_TrainingApproval hRMS_TrainingApproval)
        {
            //HRMS_TrainingApproval hRMS_TrainingApproval = employeeSelection.employeeSelection;
            var EmpDetails = db.HRMS_Emp_Details.Where(rec => rec.EMP_ID == hRMS_TrainingApproval.EMP_ID).FirstOrDefault();
            hRMS_TrainingApproval.Designation = EmpDetails.Designation;
            hRMS_TrainingApproval.EmpDept = EmpDetails.Department;
            hRMS_TrainingApproval.EmpName = EmpDetails.Display_Name;
            hRMS_TrainingApproval.Status = 2;
            hRMS_TrainingApproval.ApproveBy = Convert.ToInt64(Session["id"]);
            hRMS_TrainingApproval.ApproveDate = DateTime.Now.Date;

            var exist = db.HRMS_TrainingApproval.Where(rec => rec.EMP_ID == hRMS_TrainingApproval.EMP_ID && rec.Program_ID == hRMS_TrainingApproval.Program_ID && rec.Status ==2).FirstOrDefault();
            if (exist == null)
            {
                var existsameEmp = db.HRMS_TrainingApproval.Where(rec => rec.EMP_ID == hRMS_TrainingApproval.EMP_ID && rec.Program_ID == hRMS_TrainingApproval.Program_ID ).FirstOrDefault();
                if (existsameEmp == null)
                {
                    db.HRMS_TrainingApproval.Add(hRMS_TrainingApproval);
                    db.SaveChanges();
                    ModelState.Clear();
                    var status = "Employee Added successfullly.";
                    return Json(status, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    existsameEmp.Status = 2;
                    db.Entry(existsameEmp).State = EntityState.Modified;
                    db.SaveChanges(); ModelState.Clear();
                    var status = "Employee Added successfullly.";
                    return Json(status, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var status = "Employee is already exist for this program!";
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }
        public string UpdateEmployeedata(long id, string FromTIme, string ToTime)
        {
            TimeSpan _FromTime = TimeSpan.Parse(FromTIme);
            TimeSpan _ToTime = TimeSpan.Parse(ToTime);


            var Employee = db.HRMS_TrainingApproval.Where(rec => rec.ID == id).FirstOrDefault();

            if (Employee != null)
            {
                Employee.FromTime = _FromTime;
                Employee.ToTime = _ToTime;
                db.Entry(Employee).State = EntityState.Modified;
                db.SaveChanges(); ModelState.Clear();
                return ("Data Updated Successfully.");
            }
            return ("Data are not updated !");

        }
        public ActionResult View(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_ProgramDetail programDetail = db.HRMS_ProgramDetail.Find(id);
            if (programDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = programDetail.ID;
            ViewBag.ProgramName = programDetail.ProgramName;

            ViewBag.ProgramType = programDetail.ProgramType;

            ViewBag.TrainingType = programDetail.TrainingType;

            ViewBag.FromDate = programDetail.FromDate;

            ViewBag.ToDate = programDetail.ToDate;

            ViewBag.FromTime = programDetail.FromTime;

            ViewBag.ToTime = programDetail.ToTime;

            ViewBag.TransactionDate = programDetail.TransactionDate;


            HRMS_TrainingApproval TrainingApproval = new HRMS_TrainingApproval();
            TrainingApproval.FromDate = programDetail.FromDate;
            TrainingApproval.ToDate = programDetail.ToDate;
            TrainingApproval.Program_ID = programDetail.ID;
            TrainingApproval.FromTime = programDetail.FromTime;

            TrainingApproval.ToTime = programDetail.ToTime;

            return View(TrainingApproval);
        }
        public bool deleteEmployee(long id)
        {
            HRMS_TrainingApproval Employee = db.HRMS_TrainingApproval.Find(id);
            if (Employee != null)
            {
                Employee.Status = 3;
                db.Entry(Employee).State = EntityState.Modified;
                db.SaveChanges(); ModelState.Clear();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}