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
    public class TrainingProgramDetailController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: TrainingProgramDetail
        public ActionResult Index()
        {
            //var hRMS_ProgramDetail = db.HRMS_ProgramDetail.Include(h => h.HRMS_Training_Request_Application);
            //return View(hRMS_ProgramDetail.ToList());
            return View(db.HRMS_Training_Request_Application.ToList());
        }

        // GET: TrainingProgramDetail/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_ProgramDetail hRMS_ProgramDetail = db.HRMS_ProgramDetail.Find(id);
            if (hRMS_ProgramDetail == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_ProgramDetail);
        }

        // GET: TrainingProgramDetail/Create
        public ActionResult Create(long? id)
        {
            HRMS_ProgramDetail obj = new HRMS_ProgramDetail();
            obj.TransactionDate = DateTime.Now;
           
            ViewBag.TrainingID = new SelectList(db.HRMS_Training_Request_Application.Where(x=>x.ApplicationId==id.Value), "ApplicationId", "Training_Name");
            return View(obj);
        }

        // POST: TrainingProgramDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TransactionDate,TrainingID,ProgramName,Subject,FromDate,ToDate,FromTime,ToTime,TrainingType,ProgramType,SubjectType,Type,ProgramMode,Venue,Budget,Address,City,BenefitsToOrg,Remarks,RatingMethod,TrainingStatus,CompletedBy,CompleteDate,RemarksOther,Flag")] HRMS_ProgramDetail hRMS_ProgramDetail)
        {
            if (ModelState.IsValid)
            {
                db.HRMS_ProgramDetail.Add(hRMS_ProgramDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TrainingID = new SelectList(db.HRMS_Training_Request_Application, "ApplicationId", "Training_Name", hRMS_ProgramDetail.TrainingID);
            return View(hRMS_ProgramDetail);
        }

        // GET: TrainingProgramDetail/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_ProgramDetail hRMS_ProgramDetail = db.HRMS_ProgramDetail.Find(id);
            if (hRMS_ProgramDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrainingID = new SelectList(db.HRMS_Training_Request_Application, "ApplicationId", "Training_Name", hRMS_ProgramDetail.TrainingID);
            return View(hRMS_ProgramDetail);
        }

        // POST: TrainingProgramDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TransactionDate,TrainingID,ProgramName,Subject,FromDate,ToDate,FromTime,ToTime,TrainingType,ProgramType,SubjectType,Type,ProgramMode,Venue,Budget,Address,City,BenefitsToOrg,Remarks,RatingMethod,TrainingStatus,CompletedBy,CompleteDate,RemarksOther,Flag")] HRMS_ProgramDetail hRMS_ProgramDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hRMS_ProgramDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrainingID = new SelectList(db.HRMS_Training_Request_Application, "ApplicationId", "Training_Name", hRMS_ProgramDetail.TrainingID);
            return View(hRMS_ProgramDetail);
        }

        // GET: TrainingProgramDetail/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_ProgramDetail hRMS_ProgramDetail = db.HRMS_ProgramDetail.Find(id);
            if (hRMS_ProgramDetail == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_ProgramDetail);
        }

        // POST: TrainingProgramDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HRMS_ProgramDetail hRMS_ProgramDetail = db.HRMS_ProgramDetail.Find(id);
            db.HRMS_ProgramDetail.Remove(hRMS_ProgramDetail);
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
