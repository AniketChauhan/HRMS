using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;

namespace HRMS.Controllers
{
    [Authorize(Roles = "admin,midman")]
    public class RequestApplicationController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        public ActionResult Data(long? id)
        {
            if (id != null)
            {
                HRMS_TRAINING_SKILL_MS obj = db.HRMS_TRAINING_SKILL_MS.Where(x => x.Skill_ID == id.Value).FirstOrDefault();

                if (obj != null)
                {
                    var result = new { Skill_Name = obj.Skill_Name};
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {

            var Result1 = (from Result in db.HRMS_TRAINING_SKILL_MS
                           where Result.Skill_Name.StartsWith(prefix) 
                           select new
                           {
                               label = Result.Skill_Name,
                               val = Result.Skill_ID
                           }).ToList();

            return Json(Result1);
        }


        // GET: RequestApplication
        public ActionResult Index()
        {
            //var Request_Application = db.HRMS_Training_Request_Application.Include(h => h.Accounts).Include(h => h.Accounts1).Include(h => h.HRMS_DEPT).Include(h => h.HRMS_DESG_MS).Include(h => h.HRMS_TRAINING_SKILL_MS);
            //return View(Request_Application.ToList());


            if (User.IsInRole("admin"))
            {
                var hRMS_Training_Request_Application = db.HRMS_Training_Request_Application.Include(h => h.Accounts).Include(h => h.Accounts1).Include(h => h.HRMS_DEPT).Include(h => h.HRMS_DESG_MS).Include(h => h.HRMS_TRAINING_SKILL_MS);

                return View(hRMS_Training_Request_Application.ToList());
            }
            else if (User.IsInRole("midman"))
            {
                long emp_id = Convert.ToInt64(Session["id"]);
                var hRMS_Training_Request_Application = db.HRMS_Training_Request_Application.Include(h => h.Accounts).Include(h => h.Accounts1).Include(h => h.HRMS_DEPT).Include(h => h.HRMS_DESG_MS).Include(h => h.HRMS_TRAINING_SKILL_MS).Where(rec => rec.EmpID == emp_id);


                return View(hRMS_Training_Request_Application.ToList());
            }
            else
            {
                return HttpNotFound();
            }

        }

        // GET: RequestApplication/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_Training_Request_Application hRMS_Training_Request_Application = db.HRMS_Training_Request_Application.Find(id);
            if (hRMS_Training_Request_Application == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_Training_Request_Application);
        }

        [Authorize(Roles = "midman")]
        public ActionResult Create()
        {
            long userId = Convert.ToInt64(Session["id"]);
            HRMS_Training_Request_Application Request_Application = new HRMS_Training_Request_Application();
            HRMS_Emp_Details Emp_Details = db.HRMS_Emp_Details.FirstOrDefault(rec => rec.EMP_ID == userId);

            Request_Application.EmpID = userId;
            Request_Application.DepartmentId = Emp_Details.Department;
            Request_Application.designationID = Emp_Details.Designation;
            Request_Application.request_date = DateTime.Now.Date;
            Request_Application.EmployeeName = Emp_Details.Display_Name;
            //ViewBag.EmpID = new SelectList(db.Accounts, "ID", "UserName");
            //ViewBag.ApprovedBy_ID = new SelectList(db.Accounts, "ID", "UserName");
            ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
            ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);

            ViewBag.Skill = new SelectList(db.HRMS_TRAINING_SKILL_MS, "Skill_ID", "Skill_Name");
            return View(Request_Application);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationId,request_date,EmpID,designationID,DepartmentId,Training_Name,TrainingDetails,ApprovedDate,Status,ApprovedBy_ID,Approved_Remarks,Skill")] HRMS_Training_Request_Application hRMS_Training_Request_Application)
        {
            long emp_id = Convert.ToInt64(Session["id"]);
            HRMS_Emp_Details employeedetail = db.HRMS_Emp_Details.Where(rec => rec.EMP_ID == emp_id).FirstOrDefault();
            hRMS_Training_Request_Application.EmployeeName = employeedetail.Display_Name;
            hRMS_Training_Request_Application.designationID = employeedetail.Designation;
            hRMS_Training_Request_Application.DepartmentId = employeedetail.Department;

            if (ModelState.IsValid)
            {
                var existTrainingName = db.HRMS_Training_Request_Application.Where(rec => rec.Training_Name == hRMS_Training_Request_Application.Training_Name && rec.EmpID == hRMS_Training_Request_Application.EmpID).FirstOrDefault();
                if(existTrainingName == null) {
                    hRMS_Training_Request_Application.Status = 0;
                    
                db.HRMS_Training_Request_Application.Add(hRMS_Training_Request_Application);
                db.SaveChanges();
                    ViewBag.RequestStatus = "Training Request is Added successfully.";
                    ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
                    ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
                    ViewBag.Skill = new SelectList(db.HRMS_TRAINING_SKILL_MS, "Skill_ID", "Skill_Name");
                    return View();
                }
                else
                {
                    ViewBag.RequestStatus = "This Training Request is already exist!";
                    ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
                    ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
                    ViewBag.Skill = new SelectList(db.HRMS_TRAINING_SKILL_MS, "Skill_ID", "Skill_Name");
                    return View();

                }
            }

            //ViewBag.EmpID = new SelectList(db.Accounts, "ID", "UserName", hRMS_Training_Request_Application.EmpID);
            //ViewBag.ApprovedBy_ID = new SelectList(db.Accounts, "ID", "UserName", hRMS_Training_Request_Application.ApprovedBy_ID);
            //ViewBag.DepartmentId = new SelectList(db.HRMS_DEPT, "Dept_Id", "Dept_Name", hRMS_Training_Request_Application.DepartmentId);
            //ViewBag.designationID = new SelectList(db.HRMS_DESG_MS, "Desg_Id", "Desg_Name", hRMS_Training_Request_Application.designationID);
            //ViewBag.Skill = new SelectList(db.HRMS_TRAINING_SKILL_MS, "Skill_ID", "Skill_Name", hRMS_Training_Request_Application.Skill);
            ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
            ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
            ViewBag.Skill = new SelectList(db.HRMS_TRAINING_SKILL_MS, "Skill_ID", "Skill_Name");
            return View(hRMS_Training_Request_Application);
        }

        // GET: RequestApplication/Edit/5
        [Authorize(Roles = "admin")]

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_Training_Request_Application hRMS_Training_Request_Application = db.HRMS_Training_Request_Application.Find(id);
            var user = Convert.ToInt64(Session["id"]);
            HRMS_Emp_Details employeedetail = db.HRMS_Emp_Details.Where(rec => rec.EMP_ID == hRMS_Training_Request_Application.EmpID).FirstOrDefault();
            hRMS_Training_Request_Application.EmployeeName = employeedetail.Display_Name;
            hRMS_Training_Request_Application.ApprovedBy_ID = user;
            hRMS_Training_Request_Application.ApprovedDate = DateTime.Now;
            if (hRMS_Training_Request_Application == null)
            {
                return HttpNotFound();
            }
            ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
            ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
            //ViewBag.Skill = new SelectList(db.HRMS_TRAINING_SKILL_MS, "Skill_ID", "Skill_Name");
            //ViewBag.EmpID = new SelectList(db.Accounts, "ID", "UserName", hRMS_Training_Request_Application.EmpID);
            //ViewBag.ApprovedBy_ID = new SelectList(db.Accounts, "ID", "UserName", hRMS_Training_Request_Application.ApprovedBy_ID);
            //ViewBag.DepartmentId = new SelectList(db.HRMS_DEPT, "Dept_Id", "Dept_Name", hRMS_Training_Request_Application.DepartmentId);
            //ViewBag.designationID = new SelectList(db.HRMS_DESG_MS, "Desg_Id", "Desg_Name", hRMS_Training_Request_Application.designationID);
            ViewBag.Skill = new SelectList(db.HRMS_TRAINING_SKILL_MS, "Skill_ID", "Skill_Name", hRMS_Training_Request_Application.Skill);
            return View(hRMS_Training_Request_Application);
        }

        // POST: RequestApplication/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationId,request_date,EmpID,designationID,DepartmentId,Training_Name,TrainingDetails,ApprovedDate,Status,ApprovedBy_ID,Approved_Remarks,Skill")] HRMS_Training_Request_Application hRMS_Training_Request_Application)
        {
            HRMS_Emp_Details employeedetail = db.HRMS_Emp_Details.Where(rec => rec.EMP_ID == hRMS_Training_Request_Application.EmpID).FirstOrDefault();
            hRMS_Training_Request_Application.EmployeeName = employeedetail.Display_Name;
            hRMS_Training_Request_Application.designationID = employeedetail.Designation;
            hRMS_Training_Request_Application.DepartmentId = employeedetail.Department;

            if (ModelState.IsValid)
            {
                db.Entry(hRMS_Training_Request_Application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.EmpID = new SelectList(db.Accounts, "ID", "UserName", hRMS_Training_Request_Application.EmpID);
            //ViewBag.ApprovedBy_ID = new SelectList(db.Accounts, "ID", "UserName", hRMS_Training_Request_Application.ApprovedBy_ID);
            //ViewBag.DepartmentId = new SelectList(db.HRMS_DEPT, "Dept_Id", "Dept_Name", hRMS_Training_Request_Application.DepartmentId);
            //ViewBag.designationID = new SelectList(db.HRMS_DESG_MS, "Desg_Id", "Desg_Name", hRMS_Training_Request_Application.designationID);
            //ViewBag.Skill = new SelectList(db.HRMS_TRAINING_SKILL_MS, "Skill_ID", "Skill_Name", hRMS_Training_Request_Application.Skill);
           // ViewBag.Department = db.HRMS_DEPT.Where(rec => rec.Parent_ID != null && rec.IsActive == true);
           // ViewBag.Designation = db.HRMS_DESG_MS.Where(rec => rec.IsActive == true);
           //ViewBag.Skill = new SelectList(db.HRMS_TRAINING_SKILL_MS, "Skill_ID", "Skill_Name", hRMS_Training_Request_Application.Skill);
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "admin")]

        public ActionResult Approve(long id)
        {
            var application = db.HRMS_Training_Request_Application.Where(rec => rec.ApplicationId == id).SingleOrDefault();
            if (application.Status == 2)
            {
                return RedirectToAction("Index");
            }
            application.Status = 1;
            db.Entry(application).State = EntityState.Modified;
            db.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "admin")]

        public ActionResult Cancel(long id)
        {
            var application = db.HRMS_Training_Request_Application.Where(rec => rec.ApplicationId == id).SingleOrDefault();
            application.Status = 2;
            db.Entry(application).State = EntityState.Modified;
            db.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("Index");
        }
        
        public ActionResult RemoveApplication(long id)
        {
            HRMS_Training_Request_Application hRMS_Training_Request_Application = db.HRMS_Training_Request_Application.Find(id);
            if(hRMS_Training_Request_Application.Status == 0) { 
            db.HRMS_Training_Request_Application.Remove(hRMS_Training_Request_Application);
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            else {
                ViewBag.RequestApplicationStatus = "You can only remove panding applications";

                return RedirectToAction("Index");
            }
        }


        //// GET: RequestApplication/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HRMS_Training_Request_Application hRMS_Training_Request_Application = db.HRMS_Training_Request_Application.Find(id);
        //    if (hRMS_Training_Request_Application == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(hRMS_Training_Request_Application);
        //}

        //// POST: RequestApplication/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    HRMS_Training_Request_Application hRMS_Training_Request_Application = db.HRMS_Training_Request_Application.Find(id);
        //    db.HRMS_Training_Request_Application.Remove(hRMS_Training_Request_Application);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
