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

        [HttpGet]
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
           // obj1.HRMS_ProgramDetail.FromDate = obj.FromDate;

            //readonly dropdown
            ViewBag.Designation = new SelectList(db.HRMS_DESG_MS.Where(x=>x.Desg_Id==obj1.Designation), "Desg_Id", "Desg_Name");
            ViewBag.EmpDept = new SelectList(db.HRMS_DEPT.Where(x=>x.Dept_Id==obj1.EmpDept), "Dept_Id", "Dept_Name");
            ViewBag.Program_ID = new SelectList(db.HRMS_ProgramDetail.Where(x=>x.ID==obj1.Program_ID), "ID", "ProgramName");

            // ViewBag.xyz = new SelectList(db.HRMS_ProgramDetail.Where(x => x.ID == obj1.Program_ID), "FromDate", "FromDate");
            ViewBag.fromdate = obj.FromDate?.ToString("yyyy/MM/dd");
            ViewBag.todate = obj.ToDate?.ToString("yyyy/MM/dd");
            ViewBag.fromtime = obj.FromTime;
            ViewBag.totime = obj.ToTime;

            //NoOfDays
            TimeSpan? difference = obj.ToDate - obj.FromDate;
            ViewBag.NoOfDays = difference.Value.TotalDays;

            ViewBag.remarks = obj.Remarks;

            //already Exist or completed
            if (User.IsInRole("emp"))
            {
                bool IsExist = db.HRMS_TrainingApproval.Any(x => x.EMP_ID == obj1.EMP_ID && x.Program_ID == id.Value);
                bool IsExist1 = db.HRMS_ProgramDetail.Any(x => x.ID == id.Value && x.TrainingStatus == "Completed");
                if (IsExist || IsExist1)
                {
                    ViewBag.exist = "Exist";

                }

                HRMS_TrainingApproval status = db.HRMS_TrainingApproval.Where(x=>x.EMP_ID==obj1.EMP_ID && x.Program_ID==id.Value).FirstOrDefault();
                if (status != null)
                {
                    if (status.Status == 1)
                    {
                        ViewBag.msg = "Your Request is in Pending!";
                    }
                    else if (status.Status == 2)
                    {
                        ViewBag.msg = "Your Request is Approved!";
                    }
                    else if (status.Status == 3)
                    {
                        ViewBag.msg = "Your Request is Cancelled!";
                    }
                   
                }
            }


            return View(obj1);
        }

        //post method
        [HttpPost]
        public ActionResult Vieww(HRMS_TrainingApproval obj)
        {
            obj.Status = 1; //pending request
            db.HRMS_TrainingApproval.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
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
