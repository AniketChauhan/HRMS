using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;

namespace HRMS.Controllers
{
    public class TrainingSkillTransactionController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: HRMS_TRAINING_SKILLSET



        public ActionResult Data(long? id)
        {
            if (id != null)
            {
                HRMS_TRAINING_SKILL_MS obj = db.HRMS_TRAINING_SKILL_MS.Where(x => x.Skill_ID == id.Value).FirstOrDefault();

                if (obj != null)
                {
                    var result = new { obj.Skill_Name };
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





        public ActionResult Index()
        {
            var hRMS_TRAINING_SKILLSET = db.HRMS_TRAINING_SKILLSET.Include(h => h.HRMS_ProgramDetail).Include(h => h.HRMS_TRAINING_SKILL_MS);
            return View(hRMS_TRAINING_SKILLSET.ToList());
        }
        public ActionResult Index1()
        {
            var hRMS_ProgramDetail = db.HRMS_ProgramDetail.ToList();
            return View(hRMS_ProgramDetail);
        }
        // GET: HRMS_TRAINING_SKILLSET/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAINING_SKILLSET hRMS_TRAINING_SKILLSET = db.HRMS_TRAINING_SKILLSET.Find(id);
            if (hRMS_TRAINING_SKILLSET == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_TRAINING_SKILLSET);
        }

        // GET: HRMS_TRAINING_SKILLSET/Create
        public ActionResult Create(long? id)
        {
            // HRMS_ProgramDetail hRMS_ProgramDetail = db.HRMS_ProgramDetail.Find(id);
            //HRMS_TRAINING_SKILLSET hRMS_TRAINING_SKILLSET = db.HRMS_TRAINING_SKILLSET.Find(id);
            ViewBag.ProgDetail_ID = new SelectList(db.HRMS_ProgramDetail, "ID", "ProgramName");
            ViewBag.Skill_ID = new SelectList(db.HRMS_TRAINING_SKILL_MS, "Skill_ID", "Skill_Name");

            HRMS_ProgramDetail hRMS_ProgramDetail1 = db.HRMS_ProgramDetail.Find(id);
            HRMS_TRAINING_SKILLSET hRMS_TRAINING_SKILLSET1 = new HRMS_TRAINING_SKILLSET();
            List<HRMS_TRAINING_SKILLSET> hRMS_TRAINING_SKILLSET3 = db.HRMS_TRAINING_SKILLSET.Where(x => x.ProgDetail_ID == id).ToList();
            dynamic MultiView = new ExpandoObject();
            MultiView.hRMS_ProgramDetail = hRMS_ProgramDetail1;
            MultiView.hRMS_TRAINING_SKILLSET = hRMS_TRAINING_SKILLSET1;
            MultiView.hRMS_TRAINING_SKILLSET4 = hRMS_TRAINING_SKILLSET3;

            return View("Create", MultiView);





            //return View(hRMS_ProgramDetail);
        }

        // POST: HRMS_TRAINING_SKILLSET/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SkillSet_ID,ProgDetail_ID,Skill_ID")] HRMS_TRAINING_SKILLSET hRMS_TRAINING_SKILLSET)
        {

            ViewBag.ProgDetail_ID = new SelectList(db.HRMS_ProgramDetail, "ID", "ProgramName", hRMS_TRAINING_SKILLSET.ProgDetail_ID);
            ViewBag.Skill_ID = new SelectList(db.HRMS_TRAINING_SKILL_MS, "Skill_ID", "Skill_Name", hRMS_TRAINING_SKILLSET.Skill_ID);
            long id = Convert.ToInt64(Request["Prog_id"]);

            HRMS_ProgramDetail hRMS_ProgramDetail1 = db.HRMS_ProgramDetail.Find(id);
            HRMS_TRAINING_SKILLSET hRMS_TRAINING_SKILLSET1 = new HRMS_TRAINING_SKILLSET();
            List<HRMS_TRAINING_SKILLSET> hRMS_TRAINING_SKILLSET3 = db.HRMS_TRAINING_SKILLSET.Where(x => x.ProgDetail_ID == id).ToList();
            dynamic MultiView = new ExpandoObject();
            MultiView.hRMS_ProgramDetail = hRMS_ProgramDetail1;
            MultiView.hRMS_TRAINING_SKILLSET = hRMS_TRAINING_SKILLSET1;
            MultiView.hRMS_TRAINING_SKILLSET4 = hRMS_TRAINING_SKILLSET3;

            //            return View("Create", MultiView);
            if (ModelState.IsValid)
            {
                long a = Convert.ToInt64(Request["Prog_id"]);
                long b = Convert.ToInt64(Request["S_id"]);
                if (b != 0 && a != 0)
                {
                    ViewBag.Status = "";
                    if (!db.HRMS_TRAINING_SKILLSET.Where(x => x.ProgDetail_ID == a && x.Skill_ID == b).Any())
                    {
                        HRMS_ProgramDetail hRMS_ProgramDetaill = db.HRMS_ProgramDetail.Find(id);
                        HRMS_TRAINING_SKILL_MS hRMS_TRAINING_SKILL_MS = db.HRMS_TRAINING_SKILL_MS.Find(b);
                        hRMS_TRAINING_SKILLSET.ProgDetail_ID = Convert.ToInt64(Request["Prog_id"]);
                        hRMS_TRAINING_SKILLSET.Skill_ID = Convert.ToInt64(Request["S_id"]);
                        hRMS_TRAINING_SKILLSET.Skill_Name = hRMS_TRAINING_SKILL_MS.Skill_Name;
                        db.HRMS_TRAINING_SKILLSET.Add(hRMS_TRAINING_SKILLSET);
                        db.SaveChanges();

                        if (hRMS_ProgramDetaill.Skill == false)
                        {
                            hRMS_ProgramDetaill.Skill = true;
                            db.Entry(hRMS_ProgramDetaill).State = EntityState.Modified;
                            db.SaveChanges();
                        }


                        HRMS_ProgramDetail hRMS_ProgramDetail11 = db.HRMS_ProgramDetail.Find(id);
                        HRMS_TRAINING_SKILLSET hRMS_TRAINING_SKILLSET11 = new HRMS_TRAINING_SKILLSET();
                        List<HRMS_TRAINING_SKILLSET> hRMS_TRAINING_SKILLSET31 = db.HRMS_TRAINING_SKILLSET.Where(x => x.ProgDetail_ID == id).ToList();
                        MultiView = new ExpandoObject();
                        MultiView.hRMS_ProgramDetail = hRMS_ProgramDetail11;
                        MultiView.hRMS_TRAINING_SKILLSET = hRMS_TRAINING_SKILLSET11;
                        MultiView.hRMS_TRAINING_SKILLSET4 = hRMS_TRAINING_SKILLSET31;
                        ViewBag.Status = "New skill added successfully!!!";
                        return View("Create", MultiView);
                    }
                    ViewBag.Status = "Skill already assigned to this Program";
                    return View("Create", MultiView);
                }
                //ViewBag.Status = "Please Filled proper Data";

                //return RedirectToAction("Index");
            }
            //ViewBag.Status = "";
            return View("Create", MultiView);

        }

        // GET: HRMS_TRAINING_SKILLSET/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_TRAINING_SKILLSET hRMS_TRAINING_SKILLSET = db.HRMS_TRAINING_SKILLSET.Find(id);
            if (hRMS_TRAINING_SKILLSET == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProgDetail_ID = new SelectList(db.HRMS_ProgramDetail, "ID", "ProgramName", hRMS_TRAINING_SKILLSET.ProgDetail_ID);
            ViewBag.Skill_ID = new SelectList(db.HRMS_TRAINING_SKILL_MS, "Skill_ID", "Skill_Name", hRMS_TRAINING_SKILLSET.Skill_ID);
            return View(hRMS_TRAINING_SKILLSET);
        }

        // POST: HRMS_TRAINING_SKILLSET/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SkillSet_ID,ProgDetail_ID,Skill_ID")] HRMS_TRAINING_SKILLSET hRMS_TRAINING_SKILLSET)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hRMS_TRAINING_SKILLSET).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index1");
            }
            ViewBag.ProgDetail_ID = new SelectList(db.HRMS_ProgramDetail, "ID", "ProgramName", hRMS_TRAINING_SKILLSET.ProgDetail_ID);
            ViewBag.Skill_ID = new SelectList(db.HRMS_TRAINING_SKILL_MS, "Skill_ID", "Skill_Name", hRMS_TRAINING_SKILLSET.Skill_ID);
            return View(hRMS_TRAINING_SKILLSET);
        }

        // GET: HRMS_TRAINING_SKILLSET/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HRMS_TRAINING_SKILLSET hRMS_TRAINING_SKILLSET = db.HRMS_TRAINING_SKILLSET.Find(id);
        //    if (hRMS_TRAINING_SKILLSET == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(hRMS_TRAINING_SKILLSET);
        //}

        // POST: HRMS_TRAINING_SKILLSET/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    HRMS_TRAINING_SKILLSET hRMS_TRAINING_SKILLSET = db.HRMS_TRAINING_SKILLSET.Find(id);
        //    long P_id = hRMS_TRAINING_SKILLSET.ProgDetail_ID;
        //    db.HRMS_TRAINING_SKILLSET.Remove(hRMS_TRAINING_SKILLSET);
        //    db.SaveChanges();
        //    if (!db.HRMS_TRAINING_SKILLSET.Where(x=>x.ProgDetail_ID==P_id).Any())
        //    {
        //        HRMS_ProgramDetail hRMS_ProgramDetaill = db.HRMS_ProgramDetail.Find(P_id);
        //        if (hRMS_ProgramDetaill.Skill == true)
        //        {
        //            hRMS_ProgramDetaill.Skill = false;
        //            db.Entry(hRMS_ProgramDetaill).State = EntityState.Modified;
        //            db.SaveChanges();
        //        }
        //    }


        //    return RedirectToAction("Index1");
        //}

        public bool Delete(long id)
        {
            HRMS_TRAINING_SKILLSET hRMS_TRAINING_SKILLSET = db.HRMS_TRAINING_SKILLSET.Find(id);
            if (hRMS_TRAINING_SKILLSET != null)
            {
                long P_id = hRMS_TRAINING_SKILLSET.ProgDetail_ID;
                db.HRMS_TRAINING_SKILLSET.Remove(hRMS_TRAINING_SKILLSET);
                db.SaveChanges();
                //ViewBag.Status = "Skill Deleted!!!";
                if (!db.HRMS_TRAINING_SKILLSET.Where(x => x.ProgDetail_ID == P_id).Any())
                {
                    HRMS_ProgramDetail hRMS_ProgramDetaill = db.HRMS_ProgramDetail.Find(P_id);
                    if (hRMS_ProgramDetaill.Skill == true)
                    {
                        hRMS_ProgramDetaill.Skill = false;
                        db.Entry(hRMS_ProgramDetaill).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
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
