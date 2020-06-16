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
    public class TrainingApprovalController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: TrainingApproval
        public ActionResult Index()
        {
            if (User.IsInRole("emp"))
            {
                return View(db.HRMS_ProgramDetail.Where(x => x.TrainingStatus != "Cancel").ToList());
            }

            else
            {
                return View(db.HRMS_ProgramDetail.Where(x => x.TrainingStatus != "Cancel").ToList());
            }

         }


        //View Program
        public ActionResult View(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HRMS_ProgramDetail obj = db.HRMS_ProgramDetail.Find(id.Value);
            HRMS_TrainingApproval obj1 = new HRMS_TrainingApproval();
            obj1.EMP_ID = Convert.ToInt64(Session["id"]);

            HRMS_Emp_Details Detail = db.HRMS_Emp_Details.Where(x=>x.EMP_ID==obj1.EMP_ID).FirstOrDefault();
            obj1.Designation = Detail.Designation;
            obj1.EmpDept = Detail.Department;
            obj1.Program_ID = id.Value;
            obj1.EmpName = Detail.First_Name + " " + Detail.Last_Name;

            return View(obj1);
        }

        // GET: TrainingApproval/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TrainingApproval hRMS_TrainingApproval = db.HRMS_TrainingApproval.Find(id);
            if (hRMS_TrainingApproval == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TrainingApproval);
        }

        // GET: TrainingApproval/Create
        public ActionResult Create()
        {
            ViewBag.EMP_ID = new SelectList(db.Accounts, "ID", "UserName");
            ViewBag.EmpDept = new SelectList(db.HRMS_DEPT, "Dept_Id", "Dept_Name");
            ViewBag.Designation = new SelectList(db.HRMS_DESG_MS, "Desg_Id", "Desg_Name");
            ViewBag.Program_ID = new SelectList(db.HRMS_ProgramDetail, "ID", "ProgramName");
            return View();
        }

        // POST: TrainingApproval/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EMP_ID,Designation,Program_ID,ApproveBy,ApproveDate,Status,EmpName,EmpDept")] HRMS_TrainingApproval hRMS_TrainingApproval)
        {
            if (ModelState.IsValid)
            {
                db.HRMS_TrainingApproval.Add(hRMS_TrainingApproval);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EMP_ID = new SelectList(db.Accounts, "ID", "UserName", hRMS_TrainingApproval.EMP_ID);
            ViewBag.EmpDept = new SelectList(db.HRMS_DEPT, "Dept_Id", "Dept_Name", hRMS_TrainingApproval.EmpDept);
            ViewBag.Designation = new SelectList(db.HRMS_DESG_MS, "Desg_Id", "Desg_Name", hRMS_TrainingApproval.Designation);
            ViewBag.Program_ID = new SelectList(db.HRMS_ProgramDetail, "ID", "ProgramName", hRMS_TrainingApproval.Program_ID);
            return View(hRMS_TrainingApproval);
        }

        // GET: TrainingApproval/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TrainingApproval hRMS_TrainingApproval = db.HRMS_TrainingApproval.Find(id);
            if (hRMS_TrainingApproval == null)
            {
                return HttpNotFound();
            }
            ViewBag.EMP_ID = new SelectList(db.Accounts, "ID", "UserName", hRMS_TrainingApproval.EMP_ID);
            ViewBag.EmpDept = new SelectList(db.HRMS_DEPT, "Dept_Id", "Dept_Name", hRMS_TrainingApproval.EmpDept);
            ViewBag.Designation = new SelectList(db.HRMS_DESG_MS, "Desg_Id", "Desg_Name", hRMS_TrainingApproval.Designation);
            ViewBag.Program_ID = new SelectList(db.HRMS_ProgramDetail, "ID", "ProgramName", hRMS_TrainingApproval.Program_ID);
            return View(hRMS_TrainingApproval);
        }

        // POST: TrainingApproval/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EMP_ID,Designation,Program_ID,ApproveBy,ApproveDate,Status,EmpName,EmpDept")] HRMS_TrainingApproval hRMS_TrainingApproval)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hRMS_TrainingApproval).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EMP_ID = new SelectList(db.Accounts, "ID", "UserName", hRMS_TrainingApproval.EMP_ID);
            ViewBag.EmpDept = new SelectList(db.HRMS_DEPT, "Dept_Id", "Dept_Name", hRMS_TrainingApproval.EmpDept);
            ViewBag.Designation = new SelectList(db.HRMS_DESG_MS, "Desg_Id", "Desg_Name", hRMS_TrainingApproval.Designation);
            ViewBag.Program_ID = new SelectList(db.HRMS_ProgramDetail, "ID", "ProgramName", hRMS_TrainingApproval.Program_ID);
            return View(hRMS_TrainingApproval);
        }

        // GET: TrainingApproval/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TrainingApproval hRMS_TrainingApproval = db.HRMS_TrainingApproval.Find(id);
            if (hRMS_TrainingApproval == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TrainingApproval);
        }

        // POST: TrainingApproval/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HRMS_TrainingApproval hRMS_TrainingApproval = db.HRMS_TrainingApproval.Find(id);
            db.HRMS_TrainingApproval.Remove(hRMS_TrainingApproval);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
