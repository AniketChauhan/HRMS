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
    [Authorize(Roles = "admin")]
    public class TrainingSkillMasterController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: HRMS_TRAINING_SKILL_MS
        public ActionResult Index()
        {
            return View(db.HRMS_TRAINING_SKILL_MS.ToList());
        }

        // GET: HRMS_TRAINING_SKILL_MS/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAINING_SKILL_MS hRMS_TRAINING_SKILL_MS = db.HRMS_TRAINING_SKILL_MS.Find(id);
            if (hRMS_TRAINING_SKILL_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TRAINING_SKILL_MS);
        }

        // GET: HRMS_TRAINING_SKILL_MS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HRMS_TRAINING_SKILL_MS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Skill_ID,Skill_Name,Skill_Remark")] HRMS_TRAINING_SKILL_MS hRMS_TRAINING_SKILL_MS)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Status = "";
                var existData = db.HRMS_TRAINING_SKILL_MS.FirstOrDefault(rec => rec.Skill_Name == hRMS_TRAINING_SKILL_MS.Skill_Name && rec.Skill_Remark == hRMS_TRAINING_SKILL_MS.Skill_Remark);
                if (existData == null)
                {
                    //var ExistRemark = db.HRMS_TRAINING_SKILL_MS.FirstOrDefault(rec => rec.Skill_Remark == hRMS_TRAINING_SKILL_MS.Skill_Remark);
                    //if (ExistRemark == null)
                    //{
                    db.HRMS_TRAINING_SKILL_MS.Add(hRMS_TRAINING_SKILL_MS);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Status = "New Skill added succcessfully.";
                    return View();
                    //}
                    //ViewBag.Status = "This Remark is Already Exist ! ";
                    //return View();

                }
                ViewBag.Status = "this Skill is already exist !";
                return View();
            }

            return View(hRMS_TRAINING_SKILL_MS);
        }

        // GET: HRMS_TRAINING_SKILL_MS/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAINING_SKILL_MS hRMS_TRAINING_SKILL_MS = db.HRMS_TRAINING_SKILL_MS.Find(id);
            if (hRMS_TRAINING_SKILL_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TRAINING_SKILL_MS);
        }

        // POST: HRMS_TRAINING_SKILL_MS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Skill_ID,Skill_Name,Skill_Remark")] HRMS_TRAINING_SKILL_MS hRMS_TRAINING_SKILL_MS)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Status = "";
                var existData = db.HRMS_TRAINING_SKILL_MS.FirstOrDefault(rec => rec.Skill_Name == hRMS_TRAINING_SKILL_MS.Skill_Name && rec.Skill_Remark == hRMS_TRAINING_SKILL_MS.Skill_Remark && rec.Skill_Remark == hRMS_TRAINING_SKILL_MS.Skill_Remark && rec.Skill_ID != hRMS_TRAINING_SKILL_MS.Skill_ID);
                if (existData == null)
                {
                    //var ExistRemark = db.HRMS_TRAINING_SKILL_MS.FirstOrDefault(rec => rec.Skill_Remark == hRMS_TRAINING_SKILL_MS.Skill_Remark && rec.Skill_ID != hRMS_TRAINING_SKILL_MS.Skill_ID);
                    //if (ExistRemark == null)
                    //{
                    db.Entry(hRMS_TRAINING_SKILL_MS).State = EntityState.Modified;
                    db.SaveChanges();
                    //ModelState.Clear();
                    ViewBag.Status = "Skill Updateded succcessfully.";
                    return View();
                    //}
                    //ViewBag.Status = "This Remark is Already Exist ! ";
                    //return View();

                }
                ViewBag.Status = "this Skill is already exist !";
                return View();
            }
            return View(hRMS_TRAINING_SKILL_MS);
        }

        // GET: HRMS_TRAINING_SKILL_MS/Delete/5
        public bool Delete(long id)
        {
            HRMS_TRAINING_SKILL_MS employee_Personal_Detail = db.HRMS_TRAINING_SKILL_MS.Find(id);
            if (employee_Personal_Detail != null)
            {
                db.HRMS_TRAINING_SKILL_MS.Remove(employee_Personal_Detail);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
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
